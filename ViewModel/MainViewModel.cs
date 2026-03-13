using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MyMauiApp.ViewModel;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty] 
    string newTask = string.Empty;
    
    [ObservableProperty]
    ObservableCollection<string> tasks = new();
    
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
    
}