namespace WeatherApp.Providers.OpenWeatherMap;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOpenWeatherMapProvider(this IServiceCollection services)
    {
        services.AddHttpClient<OpenWeatherMapHttpClient>();
        services.AddKeyedScoped<IWeatherProvider, OpenWeatherMapProvider>("OpenWeatherMap");

        return services;
    }
}
