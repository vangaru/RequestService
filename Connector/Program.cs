using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RequestService.Common.Configuration;
using RequestService.Connector.Repositories;

using IHost host = CreateHostBuilder(args).Build();
using var repository = host.Services.GetRequiredService<IRabbitMqRepository>();
repository.Enqueue("Sample Message");

IHostBuilder CreateHostBuilder(string[] args)
{
    return Host.CreateDefaultBuilder(args)
        .ConfigureServices((hostContext, services) =>
        {
            services.AddTransient<IRabbitMqRepository, RabbitMqRepository>();
                
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