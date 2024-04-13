using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastrucure
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddInfrastractureservices(this IServiceCollection services , IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Database");

            // regiter serives here


            return services;
        }
    }
}
