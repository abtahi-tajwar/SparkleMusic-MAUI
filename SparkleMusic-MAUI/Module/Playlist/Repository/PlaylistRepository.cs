using System.Text.Json;
using SparkleMusic_MAUI.Module.Playlist.Entity;
using SparkleMusic_MAUI.Services;

namespace SparkleMusic_MAUI.Module.Playlist.Repository;

public class PlaylistRepository
{
    DatabaseService _databaseService;
    public PlaylistRepository(DatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    public async Task<List<PlaylistEntity>> GetDummyListAsync()
    {
        List<PlaylistEntity>? playlist = new();
        await Task.Delay(500);
        await using var stream = await FileSystem.OpenAppPackageFileAsync("musics.json");
        using var reader = new StreamReader(stream);
        string json = await reader.ReadToEndAsync();
        playlist = JsonSerializer.Deserialize<List<PlaylistEntity>>(json);
        return playlist ?? new List<PlaylistEntity>();
    }
}