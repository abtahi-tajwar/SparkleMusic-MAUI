using System.Diagnostics;
using System.Text.Json;
using SparkleMusic_MAUI.Module.Music.Entity;
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

    public async Task AddMusicToPlaylistAsync(int playlistId, int musicId)
    {
        var existingMusic = await _dbConnection.Table<PlaylistMusicEntity>().Where(item => item.MusicId == musicId).FirstOrDefaultAsync();
        if (existingMusic != null)
        {
            await _dbConnection.InsertAsync(new PlaylistMusicEntity()
            {
                MusicId = musicId,
                PlaylistId = playlistId
            });
        }
        
    }

    public async Task<List<MusicEntity>> PlaylistMusicsAsync(int playlistId)
    {
        List<MusicEntity> musics = new();
        var playlistMusicRelations = await _dbConnection.Table<PlaylistMusicEntity>().Where(item => item.PlaylistId == playlistId).ToListAsync();
        if (playlistMusicRelations == null) return musics;
        var playlistMusics = playlistMusicRelations.Select(pm => pm.MusicId);
        musics = await _dbConnection.Table<MusicEntity>().Where(m => playlistMusics.Contains(m.Id)).ToListAsync();
        return musics;
    }
}