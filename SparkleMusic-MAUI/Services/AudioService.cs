#if ANDROID
using Android.Content;
using Android.Database;
using Android.Provider;
#endif
namespace SparkleMusic_MAUI.Services;

public class AudioService
{
    public List<string> GetAllMusicFilesAsync()
    {
        List<string> musicFiles = new();
        #if ANDROID
        musicFiles = new List<string>();
        var context = Android.App.Application.Context;
        var uri = MediaStore.Audio.Media.ExternalContentUri;

        string[] projection = { MediaStore.Audio.Media.InterfaceConsts.Data };

        using (ICursor cursor = context.ContentResolver.Query(uri, projection, null, null, null))
        {
            if (cursor != null && cursor.MoveToFirst())
            {
                int columnIndex = cursor.GetColumnIndex(MediaStore.Audio.Media.InterfaceConsts.Data);
                do
                {
                    string filePath = cursor.GetString(columnIndex);
                    musicFiles.Add(filePath);
                } while (cursor.MoveToNext());

                cursor.Close();
            }
        }
        #endif
        return musicFiles;
    }
}