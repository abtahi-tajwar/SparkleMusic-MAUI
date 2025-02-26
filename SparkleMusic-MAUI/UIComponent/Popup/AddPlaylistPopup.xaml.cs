using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SparkleMusic_MAUI.Module.Playlist.Service;
using SparkleMusic_MAUI.Views.PlaylistPage;

namespace SparkleMusic_MAUI.UIComponent.Popup;

partial class AddPlaylistPopupUnwrapped : CommunityToolkit.Maui.Views.Popup
{
    private readonly PlaylistService _service;
    
    public AddPlaylistPopupUnwrapped(PlaylistService service)
    {
        InitializeComponent();
        _service = service;
        AddPlaylistSubmitBtn.Command = new Command(AddPlaylistSubmitBtn_Clicked);
    }

    private void AddPlaylistSubmitBtn_Clicked()
    {
        var value = NewPlaylistNameInput.Text;
        _ = _service.HandlePlaylistCreate(value);
    }
}

public class AddPlaylistPopup
{
    public AddPlaylistPopupUnwrapped PlaylistPopup;
    
    public AddPlaylistPopup(PlaylistService service)
    {
        PlaylistPopup = new AddPlaylistPopupUnwrapped(service);
    }
}