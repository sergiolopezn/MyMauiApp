using CommunityToolkit.Mvvm.ComponentModel;

namespace MyMauiApp.Screens.NavigationFeat;

public partial class DetailPageNavFeat : ContentPage
{
	public DetailPageNavFeat(string data)
	{
		InitializeComponent();
		constructorDataLabel.Text = $"Here's the value via constructor: {data}";
	}
	
	private async void OnBackButtonClicked(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}
	
	private async void OnBackButtonShellClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("..");
	}
	
	async void OnCloseModalButtonClicked(object sender, EventArgs e)
	{
		await Navigation.PopModalAsync();
	}
}