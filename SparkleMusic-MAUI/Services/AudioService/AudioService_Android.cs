#if ANDROID
using Android.Provider;
using SparkleMusic_MAUI.Module.Album.Entity;
using SparkleMusic_MAUI.Module.Music.Entity;
using SparkleMusic_MAUI.Utils;

namespace SparkleMusic_MAUI.Services;

public class AudioService_Android : IAudioService
{
    private async Task<bool> CheckAndRequestStoragePermission()
    {
        var status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
        if (status != PermissionStatus.Granted)
        {
            // Request permission and wait for the result
            status = await Permissions.RequestAsync<Permissions.StorageRead>();

            if (status != PermissionStatus.Granted)
            {
                return false;
            }
        }

        // Ensure we have permission
        var mediaPermissionStatus = await Permissions.CheckStatusAsync<Permissions.Media>();
        if (status != PermissionStatus.Granted)
        {
            status = await Permissions.RequestAsync<Permissions.Media>();
            if (status != PermissionStatus.Granted)
            {
                return false;
            }
        }


        return true;
    }

    private async Task<bool> CheckAndRequestMediaPermission()
    {
        var status = await Permissions.CheckStatusAsync<Permissions.Media>();
        if (status != PermissionStatus.Granted)
        {
            status = await Permissions.RequestAsync<Permissions.Media>();
            if (status != PermissionStatus.Granted)
            {
                return false;
            }
        }


        return true;
    }

    public async Task<List<MusicEntity>> GetAllMusicFilesAsync()
    {
        List<MusicEntity> musicFiles = new();

        // Check if permission is already granted

        var permissionGranted = await CheckAndRequestStoragePermission();
        if (!permissionGranted) return new List<MusicEntity>();
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


        return musicFiles;
    }

    public async Task<List<AlbumEntity>> GetAllAlbums()
    {
        List<AlbumEntity> albums = new();
        var permissionGranted = await CheckAndRequestStoragePermission();
        if (!permissionGranted) return albums;
        
        // Get Android context
        var context = Platform.CurrentActivity ?? Android.App.Application.Context;
        var uri = MediaStore.Audio.Albums.ExternalContentUri;

        string[] projection =
        {
            MediaStore.Audio.Albums.InterfaceConsts.Id,
            MediaStore.Audio.Albums.InterfaceConsts.Album,
            MediaStore.Audio.Albums.InterfaceConsts.Artist,
        };

        using (var cursor = context.ContentResolver.Query(uri, projection, null, null, MediaStore.Audio.Albums.InterfaceConsts.Album + " ASC"))
        {
            if (cursor != null && cursor.MoveToFirst())
            {
                int albumColumn = cursor.GetColumnIndex(MediaStore.Audio.Albums.InterfaceConsts.Album);
                int artistColumn = cursor.GetColumnIndex(MediaStore.Audio.Albums.InterfaceConsts.Artist);

                do
                {
                    string? albumId = cursor.GetString(albumColumn);
                    string albumName = cursor.GetString(albumColumn) ?? "Unknown Album";
                    string artistName = cursor.GetString(artistColumn) ?? "Unknown Artist";

                    albums.Add(new AlbumEntity
                    {
                        AlbumName = albumName,
                        Artist = artistName,
                        ExternalId = albumId
                    });

                } while (cursor.MoveToNext());

                cursor.Close();
            }
        }

        return albums;

    }
}
#endif