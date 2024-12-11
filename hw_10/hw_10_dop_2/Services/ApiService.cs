using System.Text.Json;
using hw_10_dop_2.Models;

namespace hw_10_dop_2.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<WeatherResponse> GetWeatherAsync(string city = "Kyiv")
    {
        try
        {
            var response = await _httpClient.GetStringAsync($"http://api.weatherapi.com/v1/forecast.json?key=7c3d112e71c34ae3af590527241112&q={city}&days=3");
        
            var data = JsonSerializer.Deserialize<WeatherResponse>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            
            return data;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }

    }
}