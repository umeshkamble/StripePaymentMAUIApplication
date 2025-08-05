using Stripe;
using Stripe.V2;
using StripeMauiApp.Services;
using StripeMauiApp.ViewModels;

namespace StripeMauiApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }

}
