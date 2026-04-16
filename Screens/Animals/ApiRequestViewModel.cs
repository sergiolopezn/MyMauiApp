using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyMauiApp.Data;
using MyMauiApp.Data.Repository.Entities;

namespace MyMauiApp.ViewModel;

public partial class ApiRequestViewModel : ObservableObject
{
    ApiRequestRepository _apiRequestRepository;

    [ObservableProperty]
    ObservableCollection<AnimalsModel> animalsData = new();
    
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
        try
        {
            var response = await _apiRequestRepository.GetDataAsync(page, limit);
            AnimalsData.Clear();
            AnimalsData = new ObservableCollection<AnimalsModel>(response ?? new List<AnimalsModel>());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching data: {ex.Message}");
        }
        finally
        {
            IsBusy = false;
        }
    }
}
