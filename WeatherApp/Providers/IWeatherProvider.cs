namespace WeatherApp.Providers;

public record Weather(
    double Temperature,       // Temperatura
    int Humidity,             // Wilgotność
    double WindSpeed,         // Prędkość wiatru
    int WindDirection,        // Kierunek wiatru
    string Description,       // Opis pogody, np. "bezchmurnie"
    string Icon               // Ikona pogodowa, np. "01d"
);

public interface IWeatherProvider
{
    Task<Weather> Get(int cityId);
}
