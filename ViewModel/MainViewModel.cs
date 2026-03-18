using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MyMauiApp.ViewModel;

public partial class MainViewModel : ObservableObject
{
    private IConnectivity connectivity;
    [ObservableProperty] string newTask = string.Empty;

    [ObservableProperty] ObservableCollection<string> tasks = new();
    
    public MainViewModel(IConnectivity connectivity)
    {
        this.connectivity = connectivity;
        checkConnection();
    }

    private void checkConnection()
    {
        if (connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            Toast.Make("No internet connection").Show();
        }
    }

    [RelayCommand]
    public void AddTask(string task)
    {
        if (!string.IsNullOrEmpty(NewTask))
        {
            Tasks.Add(NewTask);
            NewTask = string.Empty;
        }
    }

    [RelayCommand]
    public void RemoveTask(string task)
    {
        if (Tasks.Contains(task))
        {
            Tasks.Remove(task);
        }
    }

    [RelayCommand]
    async Task EditTask(string task)
    {
        await Shell.Current.GoToAsync($"{nameof(DetailPage)}?task={task}");
    }
}