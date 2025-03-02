using CommunityToolkit.Maui;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using SparkleMusic_MAUI.Module.Album.Repository;
using SparkleMusic_MAUI.Module.Music.Repository;
using SparkleMusic_MAUI.Module.Playlist.Repository;
using SparkleMusic_MAUI.Module.Playlist.Service;
using SparkleMusic_MAUI.Views.MainPage;
using SparkleMusic_MAUI.Services;
using SparkleMusic_MAUI.UIComponent.Container;
using SparkleMusic_MAUI.Views.AlbumPage;
using SparkleMusic_MAUI.Views.PlaylistPage;

namespace SparkleMusic_MAUI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseMauiCommunityToolkitMediaElement()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        
        builder.Services.AddSingleton<MusicRepository>();
        builder.Services.AddTransient<MainPageViewModel>();
        builder.Services.AddTransient<MainPage>();
        
        // Album
        builder.Services.AddTransient<AlbumPage>();
        builder.Services.AddTransient<AlbumPageViewModel>();
        builder.Services.AddSingleton<AlbumRepository>();
        
        // Playlist
        builder.Services.AddTransient<PlaylistPage>();
        builder.Services.AddTransient<PlaylistPageViewModel>();
        builder.Services.AddSingleton<PlaylistRepository>();
        builder.Services.AddSingleton<PlaylistService>();
        builder.Services.AddTransient<AddPlaylistPopup, AddPlaylistPopupViewModel>();
        builder.Services.AddSingleton<AddPlaylistPopupContext>();
        
        // Services
        builder.Services.AddSingleton<StorageService>();
        builder.Services.AddSingleton<DatabaseService>();

        #if ANDROID
        builder.Services.AddSingleton<IAudioService, AudioService_Android>();
        #else
        builder.Services.AddSingleton<IAudioService, DefaultAudioService>();
        #endif
#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}