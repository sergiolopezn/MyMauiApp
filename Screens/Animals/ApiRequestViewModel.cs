using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyMauiApp.Data;
using MyMauiApp.Data.Interfaces;
using MyMauiApp.Data.Repository.Entities;
using MyMauiApp.Screens.Animals.components;

namespace MyMauiApp.ViewModel;

public partial class ApiRequestViewModel : ObservableObject
{
    IApiRequestRepository _apiRequestRepository;
    private const int PageSize = 10;

    [ObservableProperty]
    ObservableCollection<AnimalsModel> animalsData = new();
    
    [ObservableProperty]
    bool isBusy = false;

    [ObservableProperty]
    private int currentPage = 1;

    public ApiRequestViewModel(IApiRequestRepository repository)
    {
        _apiRequestRepository = repository;
    }
	
    [RelayCommand]
    public async Task LoadData()
    {
        if (IsBusy)
            return;
        Console.WriteLine("API Called - Initial Load");
        
        IsBusy = true;
        CurrentPage = 1;
        
        try
        {
            var response = await _apiRequestRepository.GetDataAsync(CurrentPage.ToString(), PageSize.ToString());
            AnimalsData.Clear();
            if (response != null)
            {
                foreach (var item in response)
                {
                    AnimalsData.Add(item);
                }
            }
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

    [RelayCommand]
    public async Task LoadMoreData()
    {
        if (IsBusy)
            return;

        Console.WriteLine($"API Called - Loading More Data (Page {CurrentPage + 1})");
        
        IsBusy = true;
        
        try
        {
            CurrentPage++;
            var response = await _apiRequestRepository.GetDataAsync(CurrentPage.ToString(), PageSize.ToString());
            
            if (response != null && response.Count > 0)
            {
                foreach (var item in response)
                {
                    AnimalsData.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching more data: {ex.Message}");
            CurrentPage--; // Rollback page number on error
        }
        finally
        {
            IsBusy = false;
        }
    }
}
