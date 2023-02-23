using CoffeeAndWifi.WebApi.Models.Dtos;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Cafes.Net_MAUI_App.ViewModel
{
    [QueryProperty(nameof(CafeDto), nameof(CafeDto))]
    public partial class CafeDetailsViewModel : BaseViewModel
    {
        private readonly IMap _map;
        
        [ObservableProperty]
        CafeDto cafeDto;
        
        public CafeDetailsViewModel(IMap map) 
        {
            Title = "Cafe Details";
            _map = map;
        }

        [RelayCommand]
        async Task OpenMapAsync()
        {
            try
            {
                await _map.OpenAsync(Convert.ToDouble(CafeDto.Latitude), Convert.ToDouble(CafeDto.Longitude), 
                    new MapLaunchOptions
                    {
                        Name = CafeDto.CafeName,
                        NavigationMode = NavigationMode.None
                    });
            }
            catch (Exception exc)
            {
                await Shell.Current.DisplayAlert("Error", $"Unable to open map! {exc.Message}", "OK");
                return;
            }
        }
        
    }
}
