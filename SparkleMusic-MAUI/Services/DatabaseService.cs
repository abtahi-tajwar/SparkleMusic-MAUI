using SparkleMusic_MAUI.Database;
using SparkleMusic_MAUI.Module.Music.Entity;
using SQLite;

namespace SparkleMusic_MAUI.Services;

public class DatabaseService
{
    SQLiteAsyncConnection Database;

    public DatabaseService()
    {
        Init();
    }

    async Task Init()
    {
        if (Database is not null)
            return;
        
        Database = new SQLiteAsyncConnection(AppDbConfig.DatabasePath, AppDbConfig.Flags);
        var result = await Database.CreateTableAsync<MusicEntity>();
    }
}