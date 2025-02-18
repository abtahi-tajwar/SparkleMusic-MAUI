using SparkleMusic_MAUI.Database;
using SparkleMusic_MAUI.Module.Album.Entity;
using SparkleMusic_MAUI.Module.Music.Entity;
using SQLite;

namespace SparkleMusic_MAUI.Services;

public class DatabaseService
{
    public SQLiteAsyncConnection DatabaseConnection;

    public DatabaseService()
    {
        Init();
    }

    async Task Init()
    {
        if (DatabaseConnection is not null)
            return;
        
        DatabaseConnection = new SQLiteAsyncConnection(AppDbConfig.DatabasePath, AppDbConfig.Flags);
        await DatabaseConnection.CreateTableAsync<MusicEntity>();
        await DatabaseConnection.CreateTableAsync<AlbumEntity>();
    }
}