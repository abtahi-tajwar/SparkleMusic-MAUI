using System.Text.Json.Serialization;

namespace SparkleMusic_MAUI.Module.Music.Entity;

public class MusicEntity
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("title")]
    public string Title { get; set; }
    [JsonPropertyName("author")]
    public string Author { get; set; }
    [JsonPropertyName("duration")]
    public string Duration { get; set; }
    [JsonPropertyName("image")]
    public string Image { get; set; }
    [JsonPropertyName("source")]
    public string Source { get; set; }
}