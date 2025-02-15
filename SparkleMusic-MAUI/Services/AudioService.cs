using System.Collections.Generic;
using SparkleMusic_MAUI.Module.Music.Entity;
#if ANDROID
using System.Diagnostics;
using Android.Database;
using Android.Provider;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using Android;
using Android.App;
using Android.Content.PM;
using SparkleMusic_MAUI.Utils;
#endif

namespace SparkleMusic_MAUI.Services;

public class AudioService
{
    public async Task<List<MusicEntity>> GetAllMusicFilesAsync()
    {
        List<MusicEntity> musicFiles = new();

        #if ANDROID
        // Check if permission is already granted
        var status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
        if (status != PermissionStatus.Granted)
        {
            // Request permission and wait for the result
            status = await Permissions.RequestAsync<Permissions.StorageRead>();

            if (status != PermissionStatus.Granted)
            {
                Console.WriteLine("❌ Permission denied!");
                return new List<MusicEntity>(); // Return empty list if denied
            }
        }

        // Now safe to query MediaStore
        var context = Android.App.Application.Context;
        var uri = MediaStore.Audio.Media.ExternalContentUri;
        string[] projection =
        {
            MediaStore.Audio.Media.InterfaceConsts.Data,
            MediaStore.Audio.Media.InterfaceConsts.Title,
            MediaStore.Audio.Media.InterfaceConsts.Artist,
            MediaStore.Audio.Media.InterfaceConsts.Album,
            MediaStore.Audio.Media.InterfaceConsts.Duration,
            MediaStore.Audio.Media.InterfaceConsts.MimeType
        };

        using (var cursor = context.ContentResolver.Query(uri, projection, null, null, null))
        {
            if (cursor != null && cursor.MoveToFirst())
            {
                int dataColumn = cursor.GetColumnIndex(MediaStore.Audio.Media.InterfaceConsts.Data);
                int titleColumn = cursor.GetColumnIndex(MediaStore.Audio.Media.InterfaceConsts.Title);
                int artistColumn = cursor.GetColumnIndex(MediaStore.Audio.Media.InterfaceConsts.Artist);
                int albumColumn = cursor.GetColumnIndex(MediaStore.Audio.Media.InterfaceConsts.Album);
                int durationColumn = cursor.GetColumnIndex(MediaStore.Audio.Media.InterfaceConsts.Duration);

                do
                {
                    var duration = cursor.IsNull(durationColumn) ? 0 : cursor.GetLong(durationColumn);
                    var durationTimeString = Helper.ConvertToTimeStringFromMilliseconds(duration);
                    musicFiles.Add(new MusicEntity()
                    {
                        Title = cursor.GetString(titleColumn) ?? "Unknown Title",
                        Author = cursor.GetString(artistColumn) ?? "Unknown Artist",
                        Duration = durationTimeString,
                        Source = cursor.GetString(dataColumn) ?? "",
                        Image = Helper.GetRandomThumbnail()
                    });
                } while (cursor.MoveToNext());

                cursor.Close();
            }
        }
        #endif

        return musicFiles;
    }
}