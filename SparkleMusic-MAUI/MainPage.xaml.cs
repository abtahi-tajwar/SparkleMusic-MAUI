using System;
using System.Diagnostics;
using System.Windows.Input;
using CommunityToolkit.Maui.Core.Primitives;
using Microsoft.Maui.Controls;
using SparkleMusic_MAUI.Services;
using SparkleMusic_MAUI.Views.MainPage;
namespace SparkleMusic_MAUI;

public partial class MainPage : ContentPage
{
    private MainPageViewModel _viewModel;
    private readonly AudioService _audioService;
    public ICommand PlayCommand { get; }
    public MainPage(MainPageViewModel viewModel, AudioService audioService)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
        _audioService = audioService;
        // PlayCommand = new Command(ExecutePlay);
        CustomPlayButton.Command = new Command(PlayMusic);
        CustomPauseButton.Command = new Command(PauseMusic);
        _viewModel.OnPlayMusicRequested += PlayMusic;

    }

    protected override void OnAppearing()
    {
        try
        {
            _viewModel.Initialize(MyMediaElement);
            var musics = _audioService.GetAllMusicFilesAsync();
            Debug.WriteLine("MusicFetched");
        }
        catch (Exception e) 
        {
            Debug.WriteLine("Initialization Failed");
        }
    }
    

    private void PlayMusic()
    {
        _viewModel.HandlePlay();
    }

    private void PauseMusic()
    {
        _viewModel.HandlePause();
    }
}