using SparkleMusic_MAUI.Module.Album.Entity;
using SparkleMusic_MAUI.Module.Music.Entity;

namespace SparkleMusic_MAUI.Services;

public class DefaultAudioService : IAudioService
{
    public async Task<List<MusicEntity>> GetAllMusicFilesAsync()
    {
        return new List<MusicEntity>();
    }

    public async Task<List<RetreivedAlbumEntity>> RetreiveAlbumsAsync()
    {
        return new List<RetreivedAlbumEntity>();
    }
}