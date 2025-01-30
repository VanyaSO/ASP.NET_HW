namespace hw_20.Models;

public class WeatherResponse
{
    public Forecast Forecast { get; set; }
}

public class Forecast
{
    public List<ForecastDay> ForecastDay { get; set; } 
}

public class ForecastDay
{
    public DateTime Date { get; set; }
    public Day Day { get; set; }
}

public class Day
{
    public double Maxtemp_c { get; set; }
    public double Mintemp_c { get; set; }
    public Condition Condition { get; set; }
}

public class Condition
{
    public string Icon { get; set; }
}