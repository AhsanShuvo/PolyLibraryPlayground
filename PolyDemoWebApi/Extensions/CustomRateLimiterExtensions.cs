using Microsoft.AspNetCore.RateLimiting;

namespace PolyDemoWebApi.Extensions;

public static class CustomRateLimiterExtensions
{
    public static IServiceCollection AddCustomRateLimiter(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.AddFixedWindowLimiter("ThreeRequestsFiveSeconds", limiterOptions =>
            {
                limiterOptions.PermitLimit = 3;
                limiterOptions.Window = TimeSpan.FromSeconds(5);
            });
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            options.OnRejected = async (context, _) =>
            {
                await context.HttpContext.Response.WriteAsync("Too many requests have received. Request refused.",
                    CancellationToken.None);
            };
        });
        return services;
    }
}