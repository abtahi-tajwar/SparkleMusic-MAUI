using SparkleMusic_MAUI.Views.PlaylistPage;

namespace SparkleMusic_MAUI;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute("SinglePlaylistPage", typeof(SinglePlaylistPage));
    }
}