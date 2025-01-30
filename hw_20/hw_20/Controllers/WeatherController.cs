using System.Text.Json;
using hw_20.Models;
using Microsoft.AspNetCore.Mvc;

namespace hw_20.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WeatherController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetWeatherInCity(string? city, int days)
    {
        if (String.IsNullOrEmpty(city)) return NotFound(new { message = $"The city field cannot be empty" });

        try
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(
                $"http://api.weatherapi.com/v1/forecast.json?key=dba91cef02f546b8a8e134533252801&q={city}&days={days}");
            var result = JsonSerializer.Deserialize<WeatherResponse>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var weather = result?.Forecast.ForecastDay.Select(w => new WeatherTile()
            {
                Date = w.Date.ToString("dd-MM-yy"),
                MaxTemp = w.Day.Maxtemp_c,
                MinTemp = w.Day.Mintemp_c,
                Icon = w.Day.Condition.Icon
            }).ToList();

            if (weather != null && weather.Count > 0)
            {
                return Ok(weather);
            }
        }
        catch (HttpRequestException e)
        {
            return BadRequest(new { message = $"City not found" });
        }
        catch (Exception e)
        {
            return BadRequest(new { message = $"Error" });
        }

        return NotFound();
    } 
}