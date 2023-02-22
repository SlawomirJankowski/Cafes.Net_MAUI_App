using Cafes.Net_MAUI_App.ViewModel;

namespace Cafes.Net_MAUI_App;

public partial class MainPage : ContentPage
{
    private readonly CafesViewModel _viewModel;

    public MainPage(CafesViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.GetCafesCommand.Execute(this);
    }

}

