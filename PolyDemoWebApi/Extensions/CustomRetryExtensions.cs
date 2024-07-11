using Polly;
using Polly.Retry;

namespace PolyDemoWebApi.Extensions;

public static class CustomRetryExtensions
{
    public static IServiceCollection AddCustomRetry(this IServiceCollection services)
    {
        services.AddResiliencePipeline("custom_retry", builder =>
        {
            new RetryStrategyOptions()
            {
                MaxRetryAttempts = 3,
                MaxDelay = TimeSpan.FromSeconds(2)
            };
        });
        return services;
    }
}