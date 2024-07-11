using Polly;

namespace PolyDemoWebApi.Middlewares;

public class WeatherForecastRetryHandler: DelegatingHandler
{
    private readonly HttpRequestOptionsKey<Context> _executionContext = new HttpRequestOptionsKey<Context>("PolicyExecutionContext");
    
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        if (request.Options.TryGetValue(_executionContext, out var context))
        {
            if (context.TryGetValue("RetryAttempt", out var retryAttempt))
            {
                Console.WriteLine($"Added header- retry count - {retryAttempt}");
                request.Headers.Add("X-Retry-Attempt", retryAttempt.ToString());
            }
        }
        return await base.SendAsync(request, cancellationToken);
    }
}