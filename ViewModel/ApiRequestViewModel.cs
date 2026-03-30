using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyMauiApp.Data;
using MyMauiApp.ViewModel.Entity;

namespace MyMauiApp.ViewModel;

public partial class ApiRequestViewModel : ObservableObject
{
    ApiRequestRepository _apiRequestRepository;

    [ObservableProperty]
    ObservableCollection<Datum> dataArt = new();

    public ApiRequestViewModel(ApiRequestRepository repository)
    {
        _apiRequestRepository = repository;
    }
	
    [RelayCommand]
    public async Task LoadData()
    {
        Console.WriteLine("API Called");
        var page = "1"; 
        var limit = "10";
        await _apiRequestRepository.GetDataAsync(page, limit).ContinueWith(task =>
        {
            if (task.IsCompletedSuccessfully)
            {
                ApiRestResponse response = task.Result;
                DataArt.Clear();
                DataArt = new ObservableCollection<Datum>(response.data ?? new List<Datum>());
                return task.Result;
            }
            else
            {
                throw task.Exception ?? new Exception("An error occurred while fetching data.");
            }
        });
    }
}
