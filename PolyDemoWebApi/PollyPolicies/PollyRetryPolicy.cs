using Polly;
using Polly.Extensions.Http;

namespace PolyDemoWebApi.PollyPolicies;

public static class PollyRetryPolicy
{
    public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError() // handles 5XX, 408, Network Error
           // .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromMilliseconds(10));
    }
}