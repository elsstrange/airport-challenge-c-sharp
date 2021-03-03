using System;

public class Weather
{
    private Random WeatherData
    { get; set; }

    public Weather(Random weatherData)
    {
        WeatherData = weatherData;
    }

    public virtual bool IsFine()
    {
        return WeatherData.Next() != 10;
    }
}
