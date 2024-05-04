using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Messaging.Mass_Transint
{
    public static class Extensions
    {
        public static IServiceCollection AddMessageBrokers(this IServiceCollection services , IConfiguration configuration , Assembly? assembly=null)
        {
            // register services of messtransint related to RMQ

            services.AddMassTransit(config =>
            {
                config.SetKebabCaseEndpointNameFormatter();
                if (assembly is not null)
                    config.AddConsumers(assembly);

                config.UsingRabbitMq((context, config) =>
                {
                    config.Host(new Uri(configuration["MessageBroker:Host"]!), host =>
                    {
                        host.Username(configuration["MessageBroker:UserName"]!);
                        host.Password(configuration["MessageBroker:Password"]!);
                    });
                    config.ConfigureEndpoints(context);
                });

            });
            return services;
        }
    }
}
