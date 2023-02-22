using Cafes.Net_MAUI_App.Services;
using Cafes.Net_MAUI_App.View;
using CoffeeAndWifi.WebApi.Models.Dtos;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Debug = System.Diagnostics.Debug;

namespace Cafes.Net_MAUI_App.ViewModel
{
    public partial class CafesViewModel : BaseViewModel
    {
        private readonly CafeService _cafeService;
        private readonly IConnectivity _connectivity;
        private readonly IGeolocation _geolocation;

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        private ObservableCollection<CafeDto> _cafes = new ObservableCollection<CafeDto>();
        public ObservableCollection<CafeDto> Cafes
        {
            get { return _cafes; }
            set
            {
                _cafes = value;
                OnPropertyChanged(nameof(Cafes));
            }
        }

        public CafesViewModel(CafeService cafeService, IConnectivity connectivity, IGeolocation geolocation) 
        {
            _cafeService = cafeService;
            _connectivity = connectivity;
            _geolocation = geolocation;
            Title = "Cafes & WIFI";
            Cafes = _cafes;
        }

        [RelayCommand]
        private async Task GetClosestCafesAsync()
        {
            if (IsBusy || Cafes.Count == 0)
                return;
            try
            {
                var location = await _geolocation.GetLastKnownLocationAsync();
                if (location is null)
                {
                   location = await _geolocation.GetLocationAsync(
                        new GeolocationRequest
                        {
                            DesiredAccuracy =GeolocationAccuracy.Medium,
                            Timeout = TimeSpan.FromSeconds(30),
                        });
                }

                if (location is null)
                    return;

                Cafes = new ObservableCollection<CafeDto>(Cafes.OrderBy(x => location.CalculateDistance(Convert.ToDouble(x.Latitude), Convert.ToDouble(x.Longitude), DistanceUnits.Kilometers)));

            }
            catch (Exception exc)
            {
                await Shell.Current.DisplayAlert("Internet issue", $"Unable to get Cafes near your location! { exc.Message}", "OK");
                return;
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }

        [RelayCommand]
        private async Task GoToCafeDetailsAsync(CafeDto cafeDto)
        {
            if (cafeDto is null)
                return;

            await Shell.Current.GoToAsync($"{nameof(CafeDetailsPage)}", true, 
                new Dictionary<string, object>
                {
                    {"CafeDto", cafeDto}
                });
        }

        [RelayCommand]
        private async Task GetCafesAsync()
        {
            if (IsBusy)
                return;
            try
            {
                if (_connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("Internet issue", $"Check your internet connection and try again!", "OK");
                    return;
                }

                IsBusy= true;
                var response = await _cafeService.GetCafesAsync();
                if (Cafes.Count != 0)
                    Cafes.Clear();
                foreach (var cafe in response.Data)
                    Cafes.Add(cafe);
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc);
                await Shell.Current.DisplayAlert("Error", $"Unable to get Cafes: {exc.Message}", "OK");
            }
            finally 
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }

        [RelayCommand]
        private async Task GetCafesInCityAsync(string city)
        {
            if (IsBusy)
                return;
            try
            {
                if (_connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("Internet issue", $"Check your internet connection and try again!", "OK");
                    return;
                }

                IsBusy = true;
                var response = await _cafeService.GetCafesInCity(city);
                if (Cafes.Count != 0)
                    Cafes.Clear();
                foreach (var cafe in response.Data)
                    Cafes.Add(cafe);
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc);
                await Shell.Current.DisplayAlert("Error", $"Unable to get Cafes: {exc.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
