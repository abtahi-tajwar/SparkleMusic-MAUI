using System.Text.Json.Serialization;
using SQLite;

namespace SparkleMusic_MAUI.Module.Album.Entity;

public class AlbumEntity
{
    [JsonPropertyName("id")]
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    [JsonPropertyName("albumName")]
    public string AlbumName { get; set; }
    [JsonPropertyName("artist")]
    public string? Artist { get; set; }
    [JsonPropertyName("artworkUrl")]
    public string? ArtworkUrl { get; set; }
    [JsonPropertyName("externalId")]
    public string? ExternalId { get; set; }
}


public class RetreivedAlbumEntity
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    [JsonPropertyName("albumName")]
    public required string AlbumName { get; set; }
    [JsonPropertyName("artist")]
    public string? Artist { get; set; }

    public string? Artwork { get; set; }
}