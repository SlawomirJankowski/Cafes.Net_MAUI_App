using CoffeeAndWifi.WebApi.Models.Dtos;
using CoffeeAndWifi.WebApi.Models.Response;
using Newtonsoft.Json;

namespace Cafes.Net_MAUI_App.Services
{
    public class CafeService
    { 
        private readonly HttpClient _httpClient;

        private IEnumerable<CafeDto> _cafes;
        public CafeService()
        {
            _httpClient = new HttpClient
            { 
                BaseAddress = new Uri(App.BackendUrl)
            }; 
            _cafes = new List<CafeDto>();
        }

        public async Task<DataResponse<List<CafeDto>>> GetCafesAsync()
        {
            var jsonResponse = await _httpClient.GetStringAsync("GetAll");
            return JsonConvert.DeserializeObject<DataResponse<List<CafeDto>>>(jsonResponse);
        }

        public async Task<DataResponse<List<CafeDto>>> GetCafesInCity(string city)
        {
            var jsonResponse = await _httpClient.GetStringAsync($"GetCafes/{city}");
            return JsonConvert.DeserializeObject<DataResponse<List<CafeDto>>>(jsonResponse);
        }
    }
}
