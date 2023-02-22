namespace Cafes.Net_MAUI_App;

public partial class App : Application
{
    public static string BackendUrl = "http://10.0.2.2/CoffeeAndWifi/Cafe/";
    public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
}
