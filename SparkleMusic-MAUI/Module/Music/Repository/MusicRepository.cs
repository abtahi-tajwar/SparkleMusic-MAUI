using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using SparkleMusic_MAUI.Module.Music.Entity;
using SQLite;

namespace SparkleMusic_MAUI.Module.Music.Repository;

public class MusicRepository
{
    SQLiteAsyncConnection _dbContext;

    public MusicRepository(SQLiteAsyncConnection dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<MusicEntity>> GetAllPlaceholders()
    {
        await using var stream = await FileSystem.OpenAppPackageFileAsync("musics.json");
        using var reader = new StreamReader(stream);
        string json = await reader.ReadToEndAsync();
        var musics = JsonSerializer.Deserialize<List<MusicEntity>>(json);
        return musics ?? new List<MusicEntity>();
    }
    
    public async Task<List<MusicEntity>> GetAll()
    {
        return await _dbContext.Table<MusicEntity>().ToListAsync();
    }

    public async Task SaveMusicsAsync(List<MusicEntity> musics)
    {
        var existingMusics = await _dbContext.Table<MusicEntity>().ToListAsync() ?? new List<MusicEntity>();

        foreach (var music in musics)
        {
            if (existingMusics.Any(item => item.Source == music.Source) == false)
            {
                await _dbContext.InsertAsync(music);
            }
        }
    }
}