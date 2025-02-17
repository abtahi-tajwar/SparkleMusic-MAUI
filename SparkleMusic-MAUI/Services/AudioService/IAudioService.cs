using SparkleMusic_MAUI.Module.Music.Entity;

namespace SparkleMusic_MAUI.Services;

public interface IAudioService
{
    public Task<List<MusicEntity>> GetAllMusicFilesAsync();
}