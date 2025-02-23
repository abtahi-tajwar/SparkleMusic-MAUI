using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleMusic_MAUI.Views.PlaylistPage;

public partial class PlaylistPage : ContentPage
{
    private readonly PlaylistPageViewModel _viewModel;
    public PlaylistPage(PlaylistPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
        _viewModel.Initalize();
    }
}