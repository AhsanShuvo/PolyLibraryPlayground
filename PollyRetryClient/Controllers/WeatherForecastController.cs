using Microsoft.AspNetCore.Mvc;
using PollyRetryClient.Models;

namespace PollyRetryClient.Controllers;

[ApiController]
[Route("api/[controller]")]
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
    public IActionResult Get()
    {
        List<int> temp = new List<int>{ 2, 3, 4, 5, 6 };
        var ans = temp.Take(3).Concat(temp.Skip(3)).ToList();
        var response = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        });
        //throw new Exception();
        return Ok(response);
    }
}