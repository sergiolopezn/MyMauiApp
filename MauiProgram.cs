using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using MyMauiApp.Screens;
using MyMauiApp.ViewModel;
using MyMauiApp.Data;
using MyMauiApp.Data.Network;
using MyMauiApp.Screens.DataBinding;

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

		builder.Services.AddSingleton<DataBindingPage>();
		builder.Services.AddSingleton<DataBindingViewModel>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}