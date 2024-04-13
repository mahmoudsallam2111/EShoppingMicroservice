using Microsoft.Extensions.DependencyInjection;    // this namesapace come from fluentvalidation.asp.net core which exist in buliding blocks

namespace Ordering.Application
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            //add services here

            return services;
        }
    }
}
