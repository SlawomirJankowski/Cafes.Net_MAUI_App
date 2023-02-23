
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
            //Online - Get data from API
            var jsonResponse = await _httpClient.GetStringAsync("GetAll");
            return JsonConvert.DeserializeObject<DataResponse<List<CafeDto>>>(jsonResponse);

            //Offline - Get data from local Json file
            //Uncomment to check application without API
            /*
            using var stream = await FileSystem.OpenAppPackageFileAsync("cafes.json");
            using var reader = new StreamReader(stream);
            var jsonContent = await reader.ReadToEndAsync();
            return JsonConvert.DeserializeObject<DataResponse<List<CafeDto>>>(jsonContent);
            */
        }

        public async Task<DataResponse<List<CafeDto>>> GetCafesInCity(string city)
        {
            //Online - Get data from API
            var jsonResponse = await _httpClient.GetStringAsync($"GetCafes/{city}");
            return JsonConvert.DeserializeObject<DataResponse<List<CafeDto>>>(jsonResponse);

        }
    }
}
