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
    private readonly AddPlaylistPopupContext _addPlaylistPopupContext;
    public PlaylistPage(PlaylistPageViewModel viewModel, AddPlaylistPopupContext addPlaylistPopupContext)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
        _viewModel.Initalize();
        _addPlaylistPopupContext = addPlaylistPopupContext;


        OpenAddPopupButton.Command = new Command(OpenAddPopupButton_Clicked);
    }

    private void OpenAddPopupButton_Clicked()
    {
        // var popup = _addPlaylistPopup.PlaylistPopup
        // _addPlaylistPopup.Close();
        var popup = _addPlaylistPopupContext.Instantiate();
        this.ShowPopup(popup);
    }
}