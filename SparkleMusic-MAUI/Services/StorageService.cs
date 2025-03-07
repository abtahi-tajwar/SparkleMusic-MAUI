using Microsoft.Maui.Storage;
using Debug = System.Diagnostics.Debug;

namespace SparkleMusic_MAUI.Services;

public class StorageService
{
    public async Task<IEnumerable<FileResult>?> PickFilesAsync(PickOptions? providedOptions = null)
    {
        try
        {
            var customFileType = new FilePickerFileType(
                new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.iOS, ["public.audio"] }, // UTType for audio
                    { DevicePlatform.Android, ["audio/*"] }, // MIME type for all audio formats
                    {
                        DevicePlatform.WinUI, [".mp3", ".wav", ".aac", ".flac", ".m4a", ".ogg"]
                    }, // Common audio file extensions
                    { DevicePlatform.Tizen, ["audio/*"] }, // Allow all audio types
                    { DevicePlatform.macOS, ["mp3", "wav", "aiff"] }, // UTType for macOS
                    { DevicePlatform.MacCatalyst, ["public.audio"] } // UTType for macOS
                });

            PickOptions options = new()
            {
                PickerTitle = "Please select an audio file",
                FileTypes = customFileType,
            };
            // PickOptions options = new()
            // {
            //     PickerTitle = "Please select an audio file",
            //     FileTypes = FilePickerFileType.Videos,
            // };

            var result = await FilePicker.Default.PickMultipleAsync(providedOptions ?? options);
            return result;
        }
        catch (Exception ex)
        {
            // The user canceled or something went wrong
            Debug.WriteLine(ex.Message);
        }

        return null;
    }
}