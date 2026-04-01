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
    ObservableCollection<AnimalsData> animalsData = new();
    
    [ObservableProperty]
    private bool isBusy = false;

    public ApiRequestViewModel(ApiRequestRepository repository)
    {
        _apiRequestRepository = repository;
    }
	
    [RelayCommand]
    public async Task LoadData()
    {
        if (IsBusy)
            return;
        Console.WriteLine("API Called");
        var page = "1"; 
        var limit = "10";
        IsBusy = true;
        await _apiRequestRepository.GetDataAsync(page, limit).ContinueWith(task =>
        {
            IsBusy = false;
            if (task.IsCompletedSuccessfully)
            {
                var response = task.Result;
                AnimalsData.Clear();
                AnimalsData = new ObservableCollection<AnimalsData>(response ?? new List<AnimalsData>());
                return task.Result;
            }
            else
            {
                Console.WriteLine($"Error fetching data: {task.Exception?.Message}");
                throw task.Exception ?? new Exception("An error occurred while fetching data.");
            }
        });
    }
}
