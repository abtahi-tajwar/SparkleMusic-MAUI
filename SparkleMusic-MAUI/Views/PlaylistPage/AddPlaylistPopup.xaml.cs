using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using SparkleMusic_MAUI.Module.Playlist.Service;
using SparkleMusic_MAUI.Views.PlaylistPage;

namespace SparkleMusic_MAUI.Views.PlaylistPage;

partial class AddPlaylistPopup : CommunityToolkit.Maui.Views.Popup
{
    private readonly PlaylistService _service;

    public AddPlaylistPopup(AddPlaylistPopupViewModel viewModel, PlaylistService service)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _service = service;
        AddPlaylistSubmitBtn.Command = new Command(() =>
        {
            _ = viewModel.PlaylistCreateButton_Clicked(NewPlaylistNameInput.Text);
        });
    }
}