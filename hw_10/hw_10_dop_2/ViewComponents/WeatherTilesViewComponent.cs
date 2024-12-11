using hw_10_dop_2.Models;
using hw_10_dop_2.Services;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace hw_10_dop_2.ViewCompoments;

public class WeatherTilesViewComponent : ViewComponent
{
    private readonly ApiService _service;

    public WeatherTilesViewComponent(ApiService service)
    {
        _service = service;
    }
    
    public async Task<IViewComponentResult> InvokeAsync(string city)
    {
        try
        {
            var response = await _service.GetWeatherAsync(city);
            List<WeatherTile> weatherTiles = response.Forecast.ForecastDay.Select(d => new WeatherTile
            {
                Date = d.Date,
                MaxTemp = d.Day.Maxtemp_c,
                MinTemp = d.Day.Mintemp_c,
                Text = d.Day.Condition.Text,
                Icon = d.Day.Condition.Icon
            }).ToList();
        
            return await Task.FromResult((IViewComponentResult)View("WeatherTiles", weatherTiles));
        }
        catch (Exception e)
        {
            return new HtmlContentViewComponentResult(new HtmlString($"<div>{e.Message}</div>"));

        }
    }
}