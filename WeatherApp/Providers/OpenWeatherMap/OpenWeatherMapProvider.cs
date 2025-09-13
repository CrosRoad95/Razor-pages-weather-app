namespace WeatherApp.Providers.OpenWeatherMap;

public class OpenWeatherMapProvider : IWeatherProvider
{
    private readonly OpenWeatherMapHttpClient _openWeatherMapHttpClient;

    public OpenWeatherMapProvider(OpenWeatherMapHttpClient openWeatherMapHttpClient)
    {
        _openWeatherMapHttpClient = openWeatherMapHttpClient;
    }

    public Weather MapToWeather(WeatherForecastResponse forecast)
    {
        if (forecast.List == null || forecast.List.Count == 0)
            throw new InvalidOperationException("No forecast data available.");

        var firstItem = forecast.List[0];
        var weatherInfo = firstItem.Weather.FirstOrDefault();

        return new Weather(
            Temperature: firstItem.Main.Temp,
            Humidity: firstItem.Main.Humidity,
            WindSpeed: firstItem.Wind.Speed,
            WindDirection: firstItem.Wind.Deg,
            Description: weatherInfo?.Description ?? string.Empty,
            Icon: weatherInfo?.Icon ?? string.Empty
        );
    }

    public async Task<Weather> Get(int cityId)
    {
        var forecast = await _openWeatherMapHttpClient.GetWeatherAsync(cityId);
        return MapToWeather(forecast); ;
    }
}
