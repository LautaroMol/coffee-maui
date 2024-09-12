using Microsoft.Extensions.Logging;

namespace coffee_maui
{
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
                    fonts.AddFont("icons.ttf", "Icons");
                    fonts.AddFont("Rubik-Regular.ttf", "RubikRegular");
                    fonts.AddFont("Rubik-Light.ttf", "RubikLight");
                    fonts.AddFont("icons.ttf", "IconsFont");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
