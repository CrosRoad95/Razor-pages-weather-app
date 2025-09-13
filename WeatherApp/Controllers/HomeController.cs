using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WeatherApp.Models;
using WeatherApp.Providers;

namespace WeatherApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IWeatherProvider _weatherProvider;
    private readonly CitiesProvider _citiesProvider;

    public HomeController(ILogger<HomeController> logger, [FromKeyedServices("OpenWeatherMap")] IWeatherProvider weatherProvider, CitiesProvider citiesProvider)
    {
        _logger = logger;
        _weatherProvider = weatherProvider;
        _citiesProvider = citiesProvider;
    }

    public IActionResult Index()
    {
        ViewData["Cities"] = string.Join(", ", _citiesProvider.CitiesNames.Select(x => $"\"{x}\""));
        return View();
    }

    [HttpGet("/weather")]
    public async Task<IActionResult> Weather([FromQuery] string cityName)
    {
        var cityId = _citiesProvider.GetCityIdByName(cityName);
        if (cityId == null)
            return NotFound();
        var weather = await _weatherProvider.Get(cityId.Value);
        return Ok(weather);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
