using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using SparkleMusic_MAUI.Module.Music.Entity;

namespace SparkleMusic_MAUI.Module.Music.Repository;

public class MusicRepository
{
    public async Task<List<MusicEntity>> GetAll()
    {
        await using var stream = await FileSystem.OpenAppPackageFileAsync("musics.json");
        using var reader = new StreamReader(stream);
        string json = await reader.ReadToEndAsync();
        var musics = JsonSerializer.Deserialize<List<MusicEntity>>(json);
        return musics ?? new List<MusicEntity>();
    }
}