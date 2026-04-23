using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyMauiApp.Screens;
using MyMauiApp.Screens.Animals;
using MyMauiApp.Screens.DataBinding;
using MyMauiApp.Screens.NavigationFeat;
using MyMauiApp.Screens.StylesAndThemes;

namespace MyMauiApp.ViewModel;

public partial class MainViewModel : ObservableObject
{
    [RelayCommand]
    async Task NavigateToComponents()
    {
        await Shell.Current.GoToAsync(nameof(ComponentsPage));
    }
    
    [RelayCommand]
    async Task NavigateToAddTask()
    {
        await Shell.Current.GoToAsync(nameof(AddTask));
    }
    
    [RelayCommand]
    async Task NavigateHardwareScreen()
    {
        await Shell.Current.GoToAsync(nameof(PhoneResourcesPage));
    }

    [RelayCommand]
    async Task NavigateToApiRequestScreen()
    {
        await Shell.Current.GoToAsync(nameof(ApiRequestPage));
    }

    [RelayCommand]
    async Task NavigationScreen()
    {
        await Shell.Current.GoToAsync(nameof(NavigationFeaturePage));
    }

    [RelayCommand]
    async Task NavigateToDataBinding()
    {
        await Shell.Current.GoToAsync(nameof(DataBindingPage));
    }

    [RelayCommand]
    async Task NavigateToStylesAndThemes()
    {
        await Shell.Current.GoToAsync(nameof(StylesAndThemes));
    }

}