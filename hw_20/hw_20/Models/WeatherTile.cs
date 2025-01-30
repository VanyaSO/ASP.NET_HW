using System.Runtime.InteropServices.JavaScript;

namespace hw_20.Models;

public class WeatherTile
{
    public string Date { get; set; }
    public double MaxTemp { get; set; }
    public double MinTemp { get; set; }
    public string Icon { get; set; }
}