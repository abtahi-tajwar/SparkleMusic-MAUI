using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Views;
using SparkleMusic_MAUI.UIComponent.Popup;

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


        OpenAddPopupButton.Command = new Command(OpenAddPopupButton_Clicked);
    }

    private void OpenAddPopupButton_Clicked()
    {
        var popup = new AddPlaylistPopup();
        this.ShowPopup(popup);
    }
}