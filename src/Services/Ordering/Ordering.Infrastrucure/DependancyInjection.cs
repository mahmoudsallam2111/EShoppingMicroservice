using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Data;
using Ordering.Infrastrucure.Data;
using Ordering.Infrastrucure.Data.Interceptors;

namespace Ordering.Infrastrucure
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddInfrastractureservices(this IServiceCollection services , IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Database");

            // regiter serives here
            services.AddScoped<ISaveChangesInterceptor , AuditableEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor , DispatchDomainEventsInterceptor>();

            services.AddDbContext<ApplicationDbContext>((sp,options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>()); // get all services register to isavechangeinterceptor
                options.UseSqlServer(connectionString);  
            });

            services.AddScoped<IApplicationDbContext,ApplicationDbContext>();    // create an abstraction for dbcontext to use it in application layer

            return services;
        }
    }
}
