using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMauiApp.ViewModel;

namespace MyMauiApp.Screens;

public partial class PhoneResourcesPage : ContentPage
{
    public PhoneResourcesPage(PhoneResourceViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}