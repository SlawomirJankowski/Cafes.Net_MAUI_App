using Cafes.Net_MAUI_App.Services;
using Cafes.Net_MAUI_App.View;
using Cafes.Net_MAUI_App.ViewModel;
using Microsoft.Extensions.Logging;

namespace Cafes.Net_MAUI_App;

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
				fonts.AddFont("FontAwesome.otf", "FontAwesome");
			});

		builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
		builder.Services.AddSingleton<IGeolocation>(Geolocation.Default);
		builder.Services.AddSingleton<IMap>(Map.Default);

		builder.Services.AddSingleton<CafeService>();

		builder.Services.AddSingleton<CafesViewModel>();
		builder.Services.AddTransient<CafeDetailsViewModel>();

		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddTransient<CafeDetailsPage>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
