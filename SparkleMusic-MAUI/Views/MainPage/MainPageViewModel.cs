using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using SparkleMusic_MAUI.Module.Music.Entity;
using SparkleMusic_MAUI.Module.Music.Repository;

namespace SparkleMusic_MAUI.Views.MainPage;

public partial class MainPageViewModel : ObservableObject
{
    private MusicRepository _musicRepository;
    [ObservableProperty]
    private ObservableCollection<MusicEntity> musics  = new();

    public MainPageViewModel(MusicRepository musicRepository)
    {
        _musicRepository = musicRepository;
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
}