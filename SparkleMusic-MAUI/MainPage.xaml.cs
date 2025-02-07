using SparkleMusic_MAUI.Views.MainPage;

namespace SparkleMusic_MAUI;

public partial class MainPage : ContentPage
{
    private MainPageViewModel _viewModel;
    
    public MainPage(MainPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        _viewModel.Initialize();
    }
}