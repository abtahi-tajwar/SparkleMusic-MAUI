using SparkleMusic_MAUI.Module.Album.Entity;
using SparkleMusic_MAUI.Module.Music.Entity;

namespace SparkleMusic_MAUI.Services;

public interface IAudioService
{
    public Task<List<MusicEntity>> GetAllMusicFilesAsync();
    public Task<List<RetreivedAlbumEntity>> RetreiveAlbumsAsync();
}