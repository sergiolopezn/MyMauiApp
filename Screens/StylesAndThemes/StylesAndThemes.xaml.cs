using CommunityToolkit.Mvvm.Input;

namespace MyMauiApp.Screens.StylesAndThemes;

public partial class StylesAndThemes : ContentPage
{
	bool darkTheme = false;
	public StylesAndThemes()
	{
		InitializeComponent();
		Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoUnderline", (handler, view) =>
		{
			#if ANDROID
				// Remove the Android underline by setting the background tint to transparent
				handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
			#elif IOS || MACCATALYST
				// Remove the iOS border/underline by setting border style to none
				handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
			#elif WINDOWS
				// Windows requires setting the BorderThickness to 0
				handler.PlatformView.BorderThickness = new Microsoft.UI.Xaml.Thickness(0);
			#endif
		});
	}

	void SwitchTheme(object sender, ToggledEventArgs e)
	{
		Application.Current.UserAppTheme = e.Value ? AppTheme.Dark : AppTheme.Light;
	}
}