using Android.Content;
using Android.Database;
using Android.Provider;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SparkleMusic_MAUI.Services;

public class AudioServiceAndroid
{
    public async Task<List<string>> GetAllMusicFilesAsync()
    {
        List<string> musicFiles = new List<string>();
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

        return musicFiles;
    }
}