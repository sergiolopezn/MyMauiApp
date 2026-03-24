using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyMauiApp.Screens;

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
}