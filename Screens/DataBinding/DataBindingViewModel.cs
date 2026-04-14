using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MyMauiApp.Screens.DataBinding;

public partial class DataBindingViewModel: ObservableObject
{   
    public string Title { get; set; } = "Welcome to MAUI with MVVM!";

    [ObservableProperty]
    private string _displayName = "Jones Smith";
    [ObservableProperty]
    private string _productName = "Default Product";
    // [ObservableProperty] automatically creates a property "Username" 
    // that notifies the UI of changes.
    [ObservableProperty]
    string username = "John Doe";
    
    public void UpdateName()
    {
        // Changing this updates any bound UI Label automatically
        ProductName = "Updated via Code";
    }

}
