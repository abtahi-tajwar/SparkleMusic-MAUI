﻿using CommunityToolkit.Maui;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using SparkleMusic_MAUI.Module.Album.Repository;
using SparkleMusic_MAUI.Module.Music.Repository;
using SparkleMusic_MAUI.Views.MainPage;
using SparkleMusic_MAUI.Services;
using SparkleMusic_MAUI.Views.AlbumPage;

namespace SparkleMusic_MAUI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
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