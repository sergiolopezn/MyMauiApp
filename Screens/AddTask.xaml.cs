using MyMauiApp.ViewModel;

namespace MyMauiApp;

public partial class AddTask : ContentPage
{
    public AddTask(AddTaskViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}