using System.Text.Json.Serialization;
using SQLite;

namespace SparkleMusic_MAUI.Module.Playlist.Entity;

public class PlaylistEntity
{
    [JsonPropertyName("id")]
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    [JsonPropertyName("title")]
    public string Title { get; set; }
    [JsonPropertyName("thumbnail")]
    public string Thumbnail { get; set; }
    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    [JsonPropertyName("updatedAt")]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}

public class PlaylistMusicEntity
{
    [JsonPropertyName("id")]
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    
    [JsonPropertyName("playlistId")]
    public int PlaylistId { get; set; }
    
    [JsonPropertyName("musicId")]
    public int MusicId { get; set; }
}