using MyMauiApp.ViewModel;

namespace MyMauiApp.Screens;

public partial class ApiRequestPage : ContentPage
{
	public ApiRequestPage(ApiRequestViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
}