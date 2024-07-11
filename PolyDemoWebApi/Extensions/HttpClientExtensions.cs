using PolyDemoWebApi.PollyPolicies;

namespace PolyDemoWebApi.Extensions;

public static class HttpClientExtensions
{
    public static IServiceCollection AddWeatherForecastClient(this IServiceCollection services)
    {
        services.AddHttpClient("WeatherForecastClient", client =>
        {
            client.BaseAddress = new Uri("http://localhost:5161/");
        })
        .AddPolicyHandler(PollyRetryPolicy.GetRetryPolicy()); // Added retry policy for http call
        return services;
    }
}