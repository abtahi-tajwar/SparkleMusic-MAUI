using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SparkleMusic_MAUI.Module.Music.Entity;
using SparkleMusic_MAUI.Module.Music.Repository;
using SparkleMusic_MAUI.Services;

namespace SparkleMusic_MAUI.Views.MainPage;

public partial class MainPageViewModel : ObservableObject
{
    // Public 
    public event Action OnPlayMusicRequested;

    // Privates
    private readonly MusicRepository _musicRepository;
    private readonly StorageService _storageService;

    [ObservableProperty] private ObservableCollection<MusicEntity> musics = new();

    [ObservableProperty] private MediaSource currentPlayingSource;
    [ObservableProperty] private bool isPlaying;


    public MainPageViewModel(MusicRepository musicRepository, StorageService storageService)
    {
        _musicRepository = musicRepository;
        _storageService = storageService;
        CurrentPlayingSource =
            MediaSource.FromFile(
                "/storage/emulated/0/Android/data/com.companyname.sparklemusicmaui/cache/2203693cc04e0be7f4f024d5f9499e13/7b47fe1fd19d422896cff22c92669e97/sunflower-street-drumloop-85bpm-163900.mp3");
    }

    public void Initialize()
    {
        InitializeMusics();
    }

    private async void InitializeMusics()
    {
        try
        {
            var musicData = await _musicRepository.GetAll();
            Musics = new ObservableCollection<MusicEntity>(musicData);
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
        var files = await _storageService.PickFilesAsync(null);
        foreach (var file in files)
        {
            Musics.Add(new()
            {
                Id = 22,
                Title = file.FileName,
                Author = "",
                Duration = "",
                Image = "music_thumb1.png",
                Source = file.FullPath
            });
        }

        Debug.WriteLine("File Received");
    }


    public void HandlePlay(MediaElement mediaElement)
    {
        mediaElement.Play();
        IsPlaying = true;
    }public void HandlePause(MediaElement mediaElement)
    {
        mediaElement.Pause();
        IsPlaying = false;
    }

    [RelayCommand]
    private async Task OnPlayButtonClick()
    {
        Debug.WriteLine("Play");
    }

    [RelayCommand]
    private async Task OnMusicSelect(MusicEntity music)
    {
        Debug.WriteLine("MusicSelected");
        if (music.Source != null)
        {
            CurrentPlayingSource = MediaSource.FromFile(music.Source);
        }
        OnPlayMusicRequested?.Invoke();
    }
}