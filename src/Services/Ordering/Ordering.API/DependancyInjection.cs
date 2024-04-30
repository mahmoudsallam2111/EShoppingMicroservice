﻿using Carter;

namespace Ordering.API
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddCarter();
            return services;
        }

        public static WebApplication UseApiServices(this WebApplication application)
        {
            application.MapCarter();
            return application;
        }
    }
}
