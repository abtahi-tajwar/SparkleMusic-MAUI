using SparkleMusic_MAUI.Module.Album.Entity;
using SparkleMusic_MAUI.Services;
using SQLite;

namespace SparkleMusic_MAUI.Module.Album.Repository;

public class AlbumRepository
{
    IAudioService _audioService;
    private SQLiteAsyncConnection _dbConnection;
    public AlbumRepository(IAudioService audioService, DatabaseService databaseService)
    {
        _audioService = audioService;
        _dbConnection = databaseService.DatabaseConnection;
        
    }

    public async Task<List<AlbumEntity>> GetAll()
    {
        List<AlbumEntity> albums = new List<AlbumEntity>();
        var retrievedAlbums = await _audioService.RetreiveAlbumsAsync();
        albums = await _dbConnection.Table<AlbumEntity>().ToListAsync();
        foreach (var retreived in retrievedAlbums)
        {
            if (albums.All(item => item.ExternalId != retreived.Id))
            {
                var newAlbum = new AlbumEntity()
                {
                    AlbumName = retreived.AlbumName,
                    Artist = retreived.Artist,
                    ArtworkUrl = retreived.Artwork,
                    ExternalId = retreived.Id
                };
                albums.Add(newAlbum);
                await _dbConnection.InsertAsync(retreived);
            }
        }
        return albums;
    }
}