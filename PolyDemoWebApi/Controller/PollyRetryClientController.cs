using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using PolyDemoWebApi.Models;

namespace PolyDemoWebApi.Controller;

[ApiController]
[Route("api/[controller]")]
public class PollyRetryClientController: ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;

    public PollyRetryClientController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var client = _httpClientFactory.CreateClient("WeatherForecastClient");
        var request = new HttpRequestMessage(HttpMethod.Get, "api/WeatherForecast");
        var response = await client.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            var content =  await response.Content.ReadAsStringAsync();
            var weatherForecasts = JsonSerializer.Deserialize<IEnumerable<WeatherForecast>>(
                content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            return Ok(weatherForecasts);
        }

        return StatusCode((int)response.StatusCode, response.ReasonPhrase);
    }
}