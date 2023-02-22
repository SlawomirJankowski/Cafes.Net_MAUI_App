using Cafes.Net_MAUI_App.View;

namespace Cafes.Net_MAUI_App;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(CafeDetailsPage), typeof(CafeDetailsPage));
	}
}
