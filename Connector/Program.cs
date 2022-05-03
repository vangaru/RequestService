using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RequestService.Common.Configuration;
using RequestService.Common.HttpClients;
using RequestService.Connector.Repositories;
using RequestService.Connector.Services;

using IHost host = CreateHostBuilder(args).Build();
using var rabbitMqService = host.Services.GetRequiredService<IRabbitMqService>();
rabbitMqService.ReceiveRequestsAndPopulateQueue();

IHostBuilder CreateHostBuilder(string[] args)
{
    return Host.CreateDefaultBuilder(args)
        .ConfigureServices((hostContext, services) =>
        {
            services.AddTransient<IRabbitMqRepository, RabbitMqRepository>();
            services.AddTransient<IRabbitMqService, RabbitMqService>();
            services.AddTransient<IRequestsHttpClient, RequestsHttpClient>();
                
            IConfigurationSection requestsConfigurationSection =
                hostContext.Configuration.GetSection(nameof(RequestsConfiguration));
            var requestsConfiguration = requestsConfigurationSection.Get<RequestsConfiguration>();
                
            services.AddSingleton(requestsConfiguration);

            IConfigurationSection rabbitMqConfigurationSection =
                hostContext.Configuration.GetSection(nameof(RabbitMqConfiguration));
            var rabbitMqConfiguration = rabbitMqConfigurationSection.Get<RabbitMqConfiguration>();

            services.AddSingleton(rabbitMqConfiguration);
        });
}