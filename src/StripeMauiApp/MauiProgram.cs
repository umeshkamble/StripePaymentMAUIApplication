using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using StripeMauiApp.Services;
using StripeMauiApp.ViewModels;

namespace StripeMauiApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<IStripePaymentService, StripePaymentService>();
            builder.Services.AddSingletonWithShellRoute<MainPage, MainPageViewModel>("MainPageRoute");

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
