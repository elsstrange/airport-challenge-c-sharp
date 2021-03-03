using System;

public class Weather
{
    private Random WeatherData
    { get; set; }

    public Weather(Random weatherData)
    {
        WeatherData = weatherData;
    }

    public bool IsFine()
    {
        return WeatherData.Next() == 1;
    }
}
