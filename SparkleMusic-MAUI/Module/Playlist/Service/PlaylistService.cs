using SparkleMusic_MAUI.Module.Playlist.Entity;
using SparkleMusic_MAUI.Module.Playlist.Repository;
using SparkleMusic_MAUI.Utils;

namespace SparkleMusic_MAUI.Module.Playlist.Service;

public class PlaylistService(PlaylistRepository playlistRepository)
{
    private readonly PlaylistRepository _playlistRepository = playlistRepository;

    public async Task HandlePlaylistCreate(string playlistName)
    {
        await _playlistRepository.AddPlaylistAsync(new PlaylistEntity()
        {
            Title = playlistName,
            Thumbnail = Helper.GetRandomThumbnail()
        });
    }
}