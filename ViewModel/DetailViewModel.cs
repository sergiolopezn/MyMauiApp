using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MyMauiApp.ViewModel;

[QueryProperty("Task", "task")]
public partial class DetailViewModel : ObservableObject
{
    [ObservableProperty]
    string task;
    
    [RelayCommand]
    async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }
}