using System.Runtime.InteropServices.JavaScript;

namespace hw_10_dop_2.Models;

public class WeatherTile
{
    public DateTime Date { get; set; }
    public double MaxTemp { get; set; }
    public double MinTemp { get; set; }
    public string Text { get; set; }
    public string Icon { get; set; }
}