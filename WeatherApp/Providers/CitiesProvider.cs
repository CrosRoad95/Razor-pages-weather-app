using System.Text.Json;

namespace WeatherApp.Providers;

public record Coord(float Lon, float Lat);
public record City(int Id, string Name, Coord Coord);

public class CitiesProvider
{
    private readonly Dictionary<string, City> _cities;

    public IEnumerable<string> CitiesNames => _cities.Keys;

    public CitiesProvider()
    {
        // http://bulk.openweathermap.org/sample/city.list.json.gz
        using var citiesFile = File.OpenRead("cities.json");
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var cities = JsonSerializer.Deserialize<City[]>(citiesFile, options);
        _cities = cities!.GroupBy(x => x.Name)
            .Where(x => x.Count() == 1)
            .ToDictionary(x => x.Key, y => y.First());
    }

    public int? GetCityIdByName(string name) => _cities[name]?.Id;
}
