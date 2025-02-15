using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SparkleMusic_MAUI.Module.Music.Entity;
using SparkleMusic_MAUI.Module.Music.Repository;
using SparkleMusic_MAUI.Services;
using SparkleMusic_MAUI.Utils;

namespace SparkleMusic_MAUI.Views.MainPage;

public partial class MainPageViewModel : ObservableObject
{
    // Public 
    public event Action OnPlayMusicRequested;

    // Privates
    private readonly MusicRepository _musicRepository;
    private readonly StorageService _storageService;
    private readonly AudioService _audioService;

    [ObservableProperty] private ObservableCollection<MusicEntity> musics = new();

    [ObservableProperty] 
    [NotifyPropertyChangedFor(nameof(showCurrentMusicControls))]
    private MediaSource? currentPlayingSource;
    public bool showCurrentMusicControls => CurrentPlayingSource != null;
    [ObservableProperty] private double currentPlayingPositionInMilliSeconds;
    [ObservableProperty] private double currentPlayingMusicDurationInMilliSeconds;
    [ObservableProperty] private string currentPositionTimeString = "00:00";
    [ObservableProperty] private string totalDurationTimeString = "00:00";
    [ObservableProperty] private MediaElement? customMediaElement;

    [ObservableProperty] private string currentPlayingName = "Default Music";
    [ObservableProperty] private bool isPlaying;


    public MainPageViewModel(MusicRepository musicRepository, StorageService storageService, AudioService audioService)
    {
        _musicRepository = musicRepository;
        _audioService = audioService;
        _storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));
        CurrentPlayingSource = null;

    }


    private void RegisterMediaElementEvents(MediaElement element)
    {
        element.PositionChanged += ((sender, args) =>
        {
            CurrentPlayingPositionInMilliSeconds = args.Position.TotalMilliseconds;
            CurrentPositionTimeString = Helper.ConvertToTimeStringFromMilliseconds(args.Position.TotalMilliseconds);
        });
    }

    public void HandleUpdateMusicDuration(MediaElement mediaElement)
    {
        TimeSpan duration = mediaElement.Duration;
        Debug.WriteLine($"Total Duration {TotalDurationTimeString}");
        Debug.WriteLine($"Duration: {duration.ToString()}");
        CurrentPlayingMusicDurationInMilliSeconds = duration.TotalMilliseconds;
        TotalDurationTimeString = Helper.ConvertToTimeStringFromMilliseconds(duration.TotalMilliseconds);
    }

    public void HandleSeekToPosition(Slider seeker, MediaElement mediaElement)
    {
        Debug.WriteLine($"Seek to position: {seeker.Value}");
        CurrentPlayingPositionInMilliSeconds = seeker.Value;
        TimeSpan position = TimeSpan.FromMilliseconds(seeker.Value);
        mediaElement.SeekTo(position);
    }

    public void Initialize(MediaElement mediaElement)
    {
        InitializeMusics();
        CustomMediaElement = mediaElement;
        RegisterMediaElementEvents(mediaElement);
    }

    private async void InitializeMusics()
    {
        try
        {
            List<MusicEntity> musics = new();
            // var musicPlaceholderData = await _musicRepository.GetAllPlaceholders();
            var musicData = await _musicRepository.GetAll();
            var fetchedMusic = await _audioService.GetAllMusicFilesAsync();
            musics = musicData;
            foreach (var m in fetchedMusic)
            {
                if (musics.Find(item => item.Source == m.Source) == null)
                {
                    musics.Add(m);
                }
            }
            // Musics = new ObservableCollection<MusicEntity>(musicPlaceholderData.Concat(musicData).ToList());
            Musics = new ObservableCollection<MusicEntity>(musics);
            Debug.WriteLine($"Musics {Musics.Count}");
        }
        catch (Exception e)
        {
            Debug.WriteLine($"Failed to initialize musics {e.Message}");
            Console.WriteLine($"Failed to initialize musics {e.Message}");
        }
    }

    partial void OnMusicsChanged(ObservableCollection<MusicEntity> value)
    {
        Debug.WriteLine($"Total Musics {value.Count}");
        Console.WriteLine($"Total Musics {value.Count}");
    }

    [RelayCommand]
    private async void OnPlusButtonClick()
    {
        IEnumerable<FileResult>? files = null;
        files = await _storageService.PickFilesAsync(null);
        var importedMusics = new List<MusicEntity>();

        string[] thumbnails = ["music_thumb1.png", "music_thumb2.png", "music_thumb3.png", "music_thumb4.png"];
        Random random = new Random();
        foreach (var file in files)
        {
            var musicEntry = new MusicEntity()
            {
                Id = 22,
                Title = file.FileName,
                Author = "",
                Duration = "",
                Image = Helper.GetRandomThumbnail(),
                Source = file.FullPath
            };
            
            importedMusics.Add(musicEntry);
            Musics.Add(musicEntry);
        }
        
        await _musicRepository.SaveMusicsAsync(importedMusics);
        Debug.WriteLine("File Received");
    }


    public void HandlePlay()
    {
        if (CustomMediaElement == null) return;
        CustomMediaElement.Play();
        IsPlaying = true;
    }

    public void HandlePause()
    {
        if (CustomMediaElement == null) return;
        CustomMediaElement.Pause();
        IsPlaying = false;
    }
    

    [RelayCommand]
    private async Task OnMusicSelect(MusicEntity music)
    {
        Debug.WriteLine("MusicSelected");
        if (music.Source != null)
        {
            CurrentPlayingSource = MediaSource.FromFile(Uri.EscapeDataString(music.Source));
        }

        CurrentPlayingName = music.Title;
        OnPlayMusicRequested?.Invoke();
    }
}