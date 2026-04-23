using MyMauiApp.ViewModel;

namespace MyMauiApp.Screens.Animals;

public partial class ApiRequestPage : ContentPage
{
	public ApiRequestPage(ApiRequestViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
}