namespace WeatherApp.Providers.OpenWeatherMap;


public record WeatherForecastResponse(
    string Cod,
    int Message,
    int Cnt,
    List<ForecastItem> List,
    City City
);

public record ForecastItem(
    long Dt,
    MainInfo Main,
    List<WeatherInfo> Weather,
    Clouds Clouds,
    Wind Wind,
    int Visibility,
    double Pop,
    string Dt_txt
);

public record MainInfo(
    double Temp,
    double Feels_like,
    double Temp_min,
    double Temp_max,
    int Pressure,
    int Humidity
);

public record WeatherInfo(
    int Id,
    string Main,
    string Description,
    string Icon
);

public record Clouds(int All);

public record Wind(
    double Speed,
    int Deg
);

public record City(
    int Id,
    string Name,
    Coord Coord,
    string Country,
    int Timezone,
    long Sunrise,
    long Sunset
);

public record Coord(double Lat, double Lon);

public class OpenWeatherMapHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public OpenWeatherMapHttpClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration.GetValue<string>("OpenWeatherAPI")!;

        _httpClient.DefaultRequestHeaders.Add("asd", "dsa");
    }

    public async Task<WeatherForecastResponse> GetWeatherAsync(int cityId)
    {
        var path = $"http://api.openweathermap.org/data/2.5/forecast?id={cityId}&appid={_apiKey}";
        var response = await _httpClient.GetAsync(path);
        response.EnsureSuccessStatusCode();
        var forecast = await response.Content.ReadFromJsonAsync<WeatherForecastResponse>();
        return forecast;
    }
}