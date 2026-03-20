using MyMauiApp.Screens;

namespace MyMauiApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		
		Routing.RegisterRoute(nameof(AddTask), typeof(AddTask));
		Routing.RegisterRoute(nameof(DetailPage), typeof(DetailPage));
		Routing.RegisterRoute(nameof(ComponentsPage), typeof(ComponentsPage));
		
	}
}
