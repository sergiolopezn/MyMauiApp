using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using MyMauiApp.Screens;
using MyMauiApp.ViewModel;
using MyMauiApp.Data;
using MyMauiApp.Data.Network;

namespace MyMauiApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			}).UseMauiCommunityToolkit();
		
		builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
		
		// Register API services
		builder.Services.AddSingleton<ApiRestService>();
		builder.Services.AddSingleton<ApiRequestRepository>();
		
		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddSingleton<MainViewModel>();
		
		builder.Services.AddSingleton<ComponentsPage>();
		
		builder.Services.AddSingleton<AddTask>();
		builder.Services.AddSingleton<AddTaskViewModel>();
		
		builder.Services.AddSingleton<ContentPage>();
		builder.Services.AddSingleton<DetailViewModel>();
		
		builder.Services.AddSingleton<PhoneResourcesPage>();
		builder.Services.AddSingleton<PhoneResourceViewModel>();
		
		// Register API Request Page and ViewModel
		builder.Services.AddSingleton<ApiRequestPage>();
		builder.Services.AddSingleton<ApiRequestViewModel>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
// I'm android developer so I want learn maui with c# but I don't know some aspects aboud de maui framework therefore I want create a simple app where i can implement MVVM architecture, work with UI layouts, navigation, and data binding, Integrate REST APIs, use device features (camera, GPS, sensors), Implement authentication and secure storage, 
// and handle platform-specific code.
// I want to create a simple app that displays a list of items fetched from a REST API,
// allows users to view item details, and includes basic authentication.
// This will help me understand the core concepts of MAUI and how to build cross-platform applications
// using C#.