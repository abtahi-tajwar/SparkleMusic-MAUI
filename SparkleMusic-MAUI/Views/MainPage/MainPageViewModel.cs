using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.Json.Serialization;
using System.Windows.Input;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SparkleMusic_MAUI.Module.Music.Entity;
using SparkleMusic_MAUI.Module.Music.Repository;
using SparkleMusic_MAUI.Services;
using SparkleMusic_MAUI.Utils;

namespace SparkleMusic_MAUI.Views.MainPage;

class MusicViewModelDto : MusicEntity
{
    [JsonPropertyName("isSelected")] public bool IsSelected { get; set; } = false;
}

public partial class MainPageViewModel : ObservableObject
{
    // Public 
    public event Action OnPlayMusicRequested;

    // Privates
    private readonly MusicRepository _musicRepository;
    private readonly StorageService _storageService;
    private readonly IAudioService _audioService;

    [ObservableProperty] private ObservableCollection<MusicViewModelDto> musics = new();
    [ObservableProperty] private ObservableCollection<int> selectedMusics = new();
    [ObservableProperty] private bool musicInitialized = false;

    [ObservableProperty] 
    [NotifyPropertyChangedFor(nameof(showCurrentMusicControls))]
    private MediaSource? currentPlayingSource;
    public bool showCurrentMusicControls => CurrentPlayingSource != null;
    [ObservableProperty] private double currentPlayingPositionInMilliSeconds;
    [ObservableProperty] private double currentPlayingMusicDurationInMilliSeconds;
    [ObservableProperty] private string currentPositionTimeString = "00:00";
    [ObservableProperty] private string totalDurationTimeString = "00:00";
    [ObservableProperty] private bool selectModeOn = false;

    [ObservableProperty] private string currentPlayingName = "Default Music";
    [ObservableProperty] private bool isPlaying;


    public MainPageViewModel(MusicRepository musicRepository, StorageService storageService, IAudioService audioService)
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
    

    #region Initialization
    public void Initialize(MediaElement mediaElement)
    {
        InitializeMusics();
        RegisterMediaElementEvents(mediaElement);
    }

    private async void InitializeMusics()
    {
        if (MusicInitialized)
        {
            return;
        }
        try
        {
            List<MusicViewModelDto> musics = new();
            // var musicPlaceholderData = await _musicRepository.GetAllPlaceholders();
            var musicData = await _musicRepository.GetAll();
            var fetchedMusic = MusicEntityToMusicViewModelDto(await _audioService.GetAllMusicFilesAsync());
            musics = MusicEntityToMusicViewModelDto(musicData);
            foreach (var m in fetchedMusic)
            {
                if (musics.Find(item => item.Source == m.Source) == null)
                {
                    musics.Add(m);
                }
            }
            // Musics = new ObservableCollection<MusicEntity>(musicPlaceholderData.Concat(musicData).ToList());
            Musics = new ObservableCollection<MusicViewModelDto>(musics);
            Debug.WriteLine($"Musics {Musics.Count}");
            MusicInitialized = true;
        }
        catch (Exception e)
        {
            Debug.WriteLine($"Failed to initialize musics {e.Message}");
            Console.WriteLine($"Failed to initialize musics {e.Message}");
        }
    }
    #endregion

    #region Effects
    partial void OnMusicsChanged(ObservableCollection<MusicViewModelDto> value)
    {
        Debug.WriteLine($"Total Musics {value.Count}");
        Console.WriteLine($"Total Musics {value.Count}");
    }

    partial void OnSelectedMusicsChanged(ObservableCollection<int> value)
    {
        Debug.WriteLine($"Total selected musics: {value.Count}");
        Console.WriteLine($"Total selected musics: {value.Count}");
    }
    #endregion
    

    #region Handlers
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

    public void HandlePlay(MediaElement? mediaElement)
    {
        if (mediaElement == null) return;
        mediaElement.Play();
        IsPlaying = true;
    }

    public void HandlePause(MediaElement? mediaElement)
    {
        if (mediaElement == null) return;
        mediaElement.Pause();
        IsPlaying = false;
    }

    private void TurnOnSelectMode()
    {
        SelectModeOn = true;
    }
    private void TurnOffSelectMode()
    {
        SelectModeOn = false;
    }

    private void AddOrRemoveMusicToSelected(int musicId)
    {
        if (SelectedMusics.Contains(musicId))
        {
            SelectedMusics.Remove(musicId);
        }
        else
        {
            SelectedMusics.Add(musicId);
        }
    }
    #endregion
    
    

    [RelayCommand]
    private async Task OnMusicSelect(MusicEntity music)
    {
        if (SelectModeOn)
        {
            AddOrRemoveMusicToSelected(music.Id);
        }
        else
        {
            if (music.Source != null)
            {
                CurrentPlayingSource = MediaSource.FromFile(Uri.EscapeDataString(music.Source));
            }

            CurrentPlayingName = music.Title;
            OnPlayMusicRequested?.Invoke();
        }
    }
    
    [RelayCommand]
    private async Task OnPlusButtonClick()
    {
        IEnumerable<FileResult>? files = null;
        files = await _storageService.PickFilesAsync(null);
        var importedMusics = new List<MusicEntity>();
        if (files == null) return;
        string[] thumbnails = ["music_thumb1.png", "music_thumb2.png", "music_thumb3.png", "music_thumb4.png"];
        Random random = new Random();
        foreach (var file in files)
        {
            var musicEntry = new MusicViewModelDto()
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

    [RelayCommand]
    private async Task OnSelectModePress ()
    {
        TurnOnSelectMode();
    }
    [RelayCommand]
    private async Task OnCancelSelectModePress ()
    {
        TurnOffSelectMode();
    }
    [RelayCommand]
    private async Task OnMusicLongPress()
    {
        TurnOnSelectMode();
    }

    #region Helpers

    List<MusicViewModelDto> MusicEntityToMusicViewModelDto(List<MusicEntity> musicEntities)
    {
        List<MusicViewModelDto> musicDtos = musicEntities
            .Select(item => new MusicViewModelDto
            {
                Id = item.Id,
                Title = item.Title,
                Author = item.Author,
                Duration = item.Duration,
                Image = item.Image,
                Source = item.Source,
                Album = item.Album,
                IsSelected = false // New property added
            })
            .ToList();
        return musicDtos;
    }
    

    #endregion
}