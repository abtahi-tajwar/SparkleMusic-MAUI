using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SparkleMusic_MAUI.Module.Playlist.Entity;
using SparkleMusic_MAUI.Module.Playlist.Repository;
using SparkleMusic_MAUI.Utils;

namespace SparkleMusic_MAUI.Views.PlaylistPage;

public partial class PlaylistPageViewModel : ObservableObject
{
    #region PrivateMembers

    private PlaylistRepository _playlistRepository;

    #endregion

    #region Observables

    [ObservableProperty] private ObservableCollection<PlaylistEntity> playlists = new();
    [ObservableProperty] private bool initLoading = false;
    [ObservableProperty] private bool alreadyInitialized = false;

    #endregion

    public PlaylistPageViewModel(PlaylistRepository playlistRepository)
    {
        _playlistRepository = playlistRepository;
    }


    public void Initalize()
    {
        InitializePlaylists();
    }

    #region BusinessLogics

    public void InitializePlaylists()
    {
        if (!AlreadyInitialized)
        {
            _ = FetchPlaylists();
        }
    }

    public async Task FetchPlaylists()
    {
        InitLoading = true;
        // var data = await _playlistRepository.GetDummyListAsync();
        await Task.Delay(600);
        var data = await _playlistRepository.GetListAsync();
        Playlists = new ObservableCollection<PlaylistEntity>(data);
        AlreadyInitialized = true;
        InitLoading = false;
    }

    #endregion


    #region Commands
    [RelayCommand]
    private void OnRefreshButtonClicked()
    {
        _ = FetchPlaylists();
    }

    [RelayCommand]
    private void OnPlaylistListItemClicked(PlaylistEntity? playlist)
    {
        if (playlist == null) return;
        Shell.Current.GoToAsync($"SinglePlaylistPage?Id={playlist.Id}");
    }
    #endregion
}