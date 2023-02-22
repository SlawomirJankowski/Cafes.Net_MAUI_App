using Cafes.Net_MAUI_App.ViewModel;

namespace Cafes.Net_MAUI_App.View;


public partial class CafeDetailsPage : ContentPage
{

	public CafeDetailsPage(CafeDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);	
    }
}