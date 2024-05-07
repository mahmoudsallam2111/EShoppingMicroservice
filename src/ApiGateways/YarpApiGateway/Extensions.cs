using Microsoft.AspNetCore.RateLimiting;

namespace YarpApiGateway;

public static class Extensions
{
    public static IServiceCollection RegisterServices(this IServiceCollection services , IConfiguration configuration)
    {
        services.AddReverseProxy()
            .LoadFromConfig(configuration.GetSection("ReverseProxy"));

        // if we exceed the limit of request per time , the response will be unavaliable
        services.AddRateLimiter(opt =>
        {
            opt.AddFixedWindowLimiter("fixed", options =>
            {
                options.Window = TimeSpan.FromSeconds(10);
                options.PermitLimit = 5;
            });
        });
        return services;
    }

    public static WebApplication UseServices(this WebApplication app)
    {
        app.UseRateLimiter();  // this must be before mapreverseproxy pipeline

        app.MapReverseProxy();
        return app;
    }
}
