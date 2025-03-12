using System;
using System.Diagnostics;
using System.Windows.Input;
using CommunityToolkit.Maui.Core.Primitives;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls;
using SparkleMusic_MAUI.Services;
using SparkleMusic_MAUI.Views.MainPage;
namespace SparkleMusic_MAUI;

public partial class MainPage : ContentPage
{
    private MainPageViewModel _viewModel;
    private readonly IAudioService _audioService;
    
    public MainPage(MainPageViewModel viewModel, IAudioService audioService)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
        _audioService = audioService;
        // PlayCommand = new Command(ExecutePlay);
        CustomPlayButton.Command = new Command(PlayMusic);
        CustomPauseButton.Command = new Command(PauseMusic);
        _viewModel.OnPlayMusicRequested += PlayMusic;
        
        MyMediaElement.MediaOpened += async (sender, e) =>
        {
            await Task.Delay(200);
            _viewModel.HandleUpdateMusicDuration(MyMediaElement);
        };
        
        CurrentSongSeeker.DragCompletedCommand = new Command(() =>
        {
            _viewModel.HandleSeekToPosition(CurrentSongSeeker, MyMediaElement);
        });

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
        _viewModel.HandlePlay(MyMediaElement);
    }

    private void PauseMusic()
    {
        _viewModel.HandlePause(MyMediaElement);
    }
}