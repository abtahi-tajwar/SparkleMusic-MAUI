using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Views;
using SparkleMusic_MAUI.Module.Playlist.Service;

namespace SparkleMusic_MAUI.Views.PlaylistPage;

public partial class PlaylistPage : ContentPage
{
    private readonly PlaylistPageViewModel _viewModel;
    private readonly IServiceProvider _serviceProvider;
    public PlaylistPage(PlaylistPageViewModel viewModel, PlaylistService playlistService, AddPlaylistPopup addPlaylistPopup, AddPlaylistPopupViewModel _popupViewModel, IServiceProvider serviceProvider)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
        _viewModel.Initalize();
        _serviceProvider = serviceProvider;


        OpenAddPopupButton.Command = new Command(OpenAddPopupButton_Clicked);
    }

    private void OpenAddPopupButton_Clicked()
    {
        // var popup = _addPlaylistPopup.PlaylistPopup
        // _addPlaylistPopup.Close();
        var viewModel = _serviceProvider.GetRequiredService<AddPlaylistPopupViewModel>();
        var service = _serviceProvider.GetRequiredService<PlaylistService>();
        var popup = new AddPlaylistPopup(viewModel, service);
        this.ShowPopup(popup);
    }
}