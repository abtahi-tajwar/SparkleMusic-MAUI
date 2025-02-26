using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SparkleMusic_MAUI.Module.Playlist.Entity;
using SparkleMusic_MAUI.Module.Playlist.Repository;
using SparkleMusic_MAUI.UIComponent.Popup;

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
        _ = FetchPlaylists();
    }

    public async Task FetchPlaylists()
    {
        if (!AlreadyInitialized)
        {
            InitLoading = true;
            var data = await _playlistRepository.GetDummyListAsync();
            Playlists = new ObservableCollection<PlaylistEntity>(data);
            AlreadyInitialized = true;
            InitLoading = false;
        }
    }
    
}