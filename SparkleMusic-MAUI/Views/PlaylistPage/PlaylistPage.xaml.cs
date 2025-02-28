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
    private readonly PlaylistService _playlistService;
    private readonly AddPlaylistPopup _addPlaylistPopup;
    public PlaylistPage(PlaylistPageViewModel viewModel, PlaylistService playlistService, AddPlaylistPopup addPlaylistPopup)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
        _viewModel.Initalize();
        _playlistService = playlistService;
        _addPlaylistPopup = addPlaylistPopup;


        OpenAddPopupButton.Command = new Command(OpenAddPopupButton_Clicked);
    }

    private void OpenAddPopupButton_Clicked()
    {
        // var popup = _addPlaylistPopup.PlaylistPopup;
        this.ShowPopup(_addPlaylistPopup);
    }
}