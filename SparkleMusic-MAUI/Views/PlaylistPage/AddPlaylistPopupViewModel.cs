using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SparkleMusic_MAUI.Module.Playlist.Entity;
using SparkleMusic_MAUI.Module.Playlist.Repository;
using SparkleMusic_MAUI.Utils;

namespace SparkleMusic_MAUI.Views.PlaylistPage;

public partial class AddPlaylistPopupViewModel(PlaylistRepository playlistRepository) : ObservableObject
{
    PlaylistRepository _playlistRepository = playlistRepository;
    [ObservableProperty] private bool isSubmitLoading;
    
    public async Task PlaylistCreateButton_Clicked(string playlistName)
    {
        IsSubmitLoading = true;
        await _playlistRepository.AddPlaylistAsync(new PlaylistEntity()
        {
            Title = playlistName,
            Thumbnail = Helper.GetRandomThumbnail()
        });
        IsSubmitLoading = false;
    }
}