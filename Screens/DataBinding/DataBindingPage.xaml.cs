namespace MyMauiApp.Screens.DataBinding;

public partial class DataBindingPage : ContentPage
{
	public DataBindingPage(DataBindingViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;

	}
}