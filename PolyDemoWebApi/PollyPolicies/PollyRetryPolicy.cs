using Polly;
using Polly.Extensions.Http;

namespace PolyDemoWebApi.PollyPolicies;

public static class PollyRetryPolicy
{
    public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError() // handles 5XX, 408, Network Error
            .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromMilliseconds(10),
                onRetry: (outcome, timespan, retryAttempt, context) =>
                {
                    context["RetryAttempt"] = retryAttempt;
                    Console.WriteLine($"retry attempt = {retryAttempt}");
                });
    }
}