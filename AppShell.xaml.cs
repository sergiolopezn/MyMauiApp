using MyMauiApp.Screens;
using MyMauiApp.Screens.NavigationFeat;

namespace MyMauiApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		
		Routing.RegisterRoute(nameof(AddTask), typeof(AddTask));
		Routing.RegisterRoute(nameof(DetailPage), typeof(DetailPage));
		Routing.RegisterRoute(nameof(ComponentsPage), typeof(ComponentsPage));
		Routing.RegisterRoute(nameof(PhoneResourcesPage), typeof(PhoneResourcesPage));
		Routing.RegisterRoute(nameof(ApiRequestPage), typeof(ApiRequestPage));
		Routing.RegisterRoute(nameof(NavigationFeaturePage), typeof(NavigationFeaturePage));
		Routing.RegisterRoute(nameof(DetailPageNavFeat), typeof(DetailPageNavFeat));
	}
}
