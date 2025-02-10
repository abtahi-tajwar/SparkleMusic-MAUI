using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SparkleMusic_MAUI.Module.Music.Entity;
using SparkleMusic_MAUI.Module.Music.Repository;
using SparkleMusic_MAUI.Services;

namespace SparkleMusic_MAUI.Views.MainPage;

public partial class MainPageViewModel : ObservableObject
{
    private readonly MusicRepository _musicRepository;
    private readonly StorageService _storageService;
    
    [ObservableProperty]
    private ObservableCollection<MusicEntity> musics  = new();
    

    public MainPageViewModel(MusicRepository musicRepository, StorageService storageService)
    {
        _musicRepository = musicRepository;
        _storageService = storageService;
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
}