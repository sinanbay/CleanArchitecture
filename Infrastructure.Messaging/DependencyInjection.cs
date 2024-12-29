using Infrastructure.Messaging;
using MassTransit;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        /*
         
              "RabbitMqSettingsDTO": {
                "Uri": "localhost",
                "UserName": "admin",
                "Password": "123456",
                "QueueName": "score-card-name"
              }
         */
        var rabbitMqSettings = configuration.GetSection(nameof(RabbitMqSettingsDTO)).Get<RabbitMqSettingsDTO>();
        services.AddMassTransit(mt =>
                        mt.UsingRabbitMq((cntxt, cfg) =>
                        {
                            cfg.Host(rabbitMqSettings.Uri, "/", c =>
                            {
                                c.Username(rabbitMqSettings.UserName);
                                c.Password(rabbitMqSettings.Password);
                            });
                        }));

        return services;
    }
}


