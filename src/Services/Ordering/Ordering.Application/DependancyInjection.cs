using BuildingBlocks.Behavoirs;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;    // this namesapace come from fluentvalidation.asp.net core which exist in buliding blocks

namespace Ordering.Application
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            //add services here
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                configuration.AddOpenBehavior(typeof(LoggingBehavoir<,>));
                configuration.AddOpenBehavior(typeof(ValidationBehavoir<,>));
            });


            return services;
        }
    }
}
