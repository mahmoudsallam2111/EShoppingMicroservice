using BuildingBlocks.Behavoirs;
using BuildingBlocks.Messaging.Mass_Transint;
using MapsterMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
using System.Reflection;    // this namesapace come from fluentvalidation.asp.net core which exist in buliding blocks

namespace Ordering.Application
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services , IConfiguration configuration)
        {
            //add services here
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                configuration.AddOpenBehavior(typeof(LoggingBehavoir<,>));
                configuration.AddOpenBehavior(typeof(ValidationBehavoir<,>));
            });

            services.AddFeatureManagement();   // register feature management

            services.AddMessageBrokers(configuration , Assembly.GetExecutingAssembly());   // this for register consumer where assymbly is not null

            return services;
        }
    }
}
