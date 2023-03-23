using RabbitBlazor.Common;
using RabbitBlazor.Services;
using RabbitBlazor.Services.Base;
using RabbitMQ.Client;

namespace RabbitBlazor;

public static class ServiceExtensions
{
    public static IServiceCollection AddRabbitMq(this IServiceCollection services)
    {
        return services.AddSingleton(serviceProvider => new ConnectionFactory
        {
            HostName = "localhost"
            //Uri = new Uri("localhost")
        });
    }

    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<MessagingService>();
        services.AddScoped<QueueService>();
        services.AddScoped<RabbitMqHttpClient>();
        services.AddScoped<ExchangeService>();
        services.Configure<RabbitMqSettings>(configuration.GetSection(nameof(RabbitMqSettings)));

        return services;
    }
}