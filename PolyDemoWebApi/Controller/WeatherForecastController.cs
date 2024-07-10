using Microsoft.AspNetCore.Mvc;
using PolyDemoWebApi.Models;
using Microsoft.AspNetCore.RateLimiting;

namespace PolyDemoWebApi.Controller;

[ApiController]
[Route("api/[controller]")]
[EnableRateLimiting("ThreeRequestsFiveSeconds")]
public class WeatherForecastController: ControllerBase
{
    private readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    
    private readonly ILogger<WeatherForecastController> _logger;
    
    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        List<int> temp = new List<int>{ 2, 3, 4, 5, 6 };
        var ans = temp.Take(3).Concat(temp.Skip(3)).ToList();
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
}