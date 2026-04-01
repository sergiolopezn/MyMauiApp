using MyMauiApp.ViewModel;

namespace MyMauiApp.Screens;

public partial class ApiRequestPage : ContentPage
{
	public ApiRequestPage(ApiRequestViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}

	public ImageSource? ConvertThumbnail(object value)
	{
        if (value is string base64String && !string.IsNullOrEmpty(base64String))
		{
			// remove the data URI prefix if it exists
			if (base64String.StartsWith("data:image"))
			{
				var commaIndex = base64String.IndexOf(',');
				if (commaIndex >= 0)
				{
					base64String = base64String.Substring(commaIndex + 1);
				}
				byte[] imageBytes = Convert.FromBase64String(base64String);
			
				return ImageSource.FromStream(() => new MemoryStream(imageBytes));
			}
		}
		return null; // or return a default image
	}
}