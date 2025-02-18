using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleMusic_MAUI.Views.AlbumPage;

public partial class AlbumPage : ContentPage
{
    private AlbumPageViewModel _viewModel;
    public AlbumPage(AlbumPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        _ = _viewModel.Initialize();
        base.OnAppearing();
    }
}