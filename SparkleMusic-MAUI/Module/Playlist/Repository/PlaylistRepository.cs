using System.Diagnostics;
using System.Text.Json;
using SparkleMusic_MAUI.Module.Playlist.Entity;
using SparkleMusic_MAUI.Services;
using SQLite;

namespace SparkleMusic_MAUI.Module.Playlist.Repository;

public class PlaylistRepository
{
    private readonly DatabaseService _databaseService;
    private readonly SQLiteAsyncConnection _dbConnection;
    public PlaylistRepository(DatabaseService databaseService)
    {
        _databaseService = databaseService;
        _dbConnection = _databaseService.DatabaseConnection;
    }

    public async Task<List<PlaylistEntity>> GetDummyListAsync()
    {
        List<PlaylistEntity>? playlist = new();
        await Task.Delay(500);
        await using var stream = await FileSystem.OpenAppPackageFileAsync("playlists.json");
        using var reader = new StreamReader(stream);
        string json = await reader.ReadToEndAsync();
        playlist = JsonSerializer.Deserialize<List<PlaylistEntity>>(json);
        return playlist ?? new List<PlaylistEntity>();
    }

    public async Task<List<PlaylistEntity>> GetListAsync()
    {
        List<PlaylistEntity>? playlist = await _dbConnection.Table<PlaylistEntity>().ToListAsync();
        return playlist ?? new List<PlaylistEntity>();
    }
    public async Task AddPlaylistAsync(PlaylistEntity playlist)
    {
        await _dbConnection.InsertAsync(playlist);
        Debug.WriteLine("New Playlist Added");
    }
}