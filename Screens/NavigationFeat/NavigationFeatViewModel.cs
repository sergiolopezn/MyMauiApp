using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MyMauiApp.Screens.NavigationFeat;

public partial class NavigationFeatViewModel : ObservableObject
{
    // Option 1. Pass Data via Constructor 
    String dataOption1 = "This is data passed via constructor";
    
    [RelayCommand]
    async Task GoToDetailPage()
    {
        
        // 2. Pass Data via BindingContext
        var detailsViewModel = new DetailPageNavFeatViewModel() { Name = "John Doe" };
        var detailsPage = new DetailPageNavFeat(dataOption1);
        detailsPage.BindingContext = detailsViewModel;
        await Application.Current.MainPage.Navigation.PushAsync(detailsPage);
    }

    [RelayCommand]
    async Task GoToDetailPageShell()
    {
        await Shell.Current.GoToAsync($"{nameof(DetailPage)}?task={dataOption1}");
    }
    
    [RelayCommand]
    async Task OpenModalNavigation()
    {
        // 2. Pass Data via BindingContext
        var detailsViewModel = new DetailPageNavFeatViewModel() { Name = "John Doe" };
        var detailsPage = new DetailPageNavFeat(dataOption1);
        detailsPage.BindingContext = detailsViewModel;
        await Application.Current.MainPage.Navigation.PushModalAsync(detailsPage);
    }
}
