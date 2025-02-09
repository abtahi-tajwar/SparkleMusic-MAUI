using System.Diagnostics;
using SparkleMusic_MAUI.Services;
using SparkleMusic_MAUI.Views.MainPage;
namespace SparkleMusic_MAUI;

public partial class MainPage : ContentPage
{
    private MainPageViewModel _viewModel;
    private readonly AudioService _audioService;
    public MainPage(MainPageViewModel viewModel, AudioService audioService)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
        _audioService = audioService;
    }

    protected override void OnAppearing()
    {
        try
        {
            _viewModel.Initialize();
            var musics = _audioService.GetAllMusicFilesAsync();
            Debug.WriteLine("MusicFetched");
        }
        catch (Exception e)
        {
            Debug.WriteLine("Initialization Failed");
        }
    }
}