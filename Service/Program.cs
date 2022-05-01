using Microsoft.EntityFrameworkCore;
using RequestService;
using RequestService.Api.Configuration;
using RequestService.Api.Services;
using RequestService.Data;
using RequestService.Repositories;
using RequestService.Services;

const string defaultConnectionProp = "DefaultConnection";

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        var optionsBuilder = new DbContextOptionsBuilder<RequestsContext>();
        optionsBuilder.UseNpgsql(hostContext.Configuration.GetConnectionString(defaultConnectionProp));
        services.AddScoped(_ => new RequestsContext(optionsBuilder.Options));

        IConfigurationSection requestsConfigurationSection = hostContext.Configuration.GetSection(nameof(RequestsConfiguration));
        var requestsConfiguration = requestsConfigurationSection.Get<RequestsConfiguration>();
        
        services.AddSingleton(requestsConfiguration);
        
        services.AddTransient<IIntensityService, IntensityService>();
        services.AddTransient<IIntervalService, IntervalService>();
        services.AddTransient<IPostgresRepository, PostgresRepository>();
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();