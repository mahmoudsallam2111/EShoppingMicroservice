using BuildingBlocks.Exceptions.Handler;
using Carter;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Ordering.API
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddCarter();

            services.AddHealthChecks()
                .AddSqlServer(configuration.GetConnectionString("Database")!);

            services.AddEndpointsApiExplorer();

            services.AddExceptionHandler<CustomExceptionHandler>();   // add custom exception handler from bulding blocks



            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("V1",
                    new() { Title = "Orders API", Version = "V1" });
            });


            return services;
        }

        public static WebApplication UseApiServices(this WebApplication application)
        {
            application.MapCarter();

            application.UseExceptionHandler(opt => { });    // add to pipe line
                                                            // 
            application.UseHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            application.UseSwagger();
            application.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "v1");
            });

            return application;
        }
    }
}
