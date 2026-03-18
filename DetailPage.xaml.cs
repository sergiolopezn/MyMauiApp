using MyMauiApp.ViewModel;

namespace MyMauiApp;

public partial class DetailPage : ContentPage
{
    public DetailPage(DetailViewModel detailViewModel)
    {
        InitializeComponent();
        BindingContext = detailViewModel;
    }
}