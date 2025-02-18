using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics;
using SparkleMusic_MAUI.Module.Album.Repository;

namespace SparkleMusic_MAUI.Views.AlbumPage;

public partial class AlbumPageViewModel : ObservableObject
{
    AlbumRepository _repository;
    public AlbumPageViewModel(AlbumRepository repository)
    {
        _repository = repository;
    }

    public async Task Initialize()
    {
        var albums = await _repository.GetAll();
        Debug.WriteLine("Fetching albums");
    }
}