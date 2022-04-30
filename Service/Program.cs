using Microsoft.EntityFrameworkCore;
using RequestService.Api.Services;
using RequestService.Data;
using RequestService.Services;
using Service;

const string defaultConnectionProp = "DefaultConnection";

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        var optionsBuilder = new DbContextOptionsBuilder<RequestsContext>();
        optionsBuilder.UseNpgsql(hostContext.Configuration.GetConnectionString(defaultConnectionProp));
        services.AddScoped(_ => new RequestsContext(optionsBuilder.Options));
        services.AddOptions();
        services.AddTransient<IIntensityService, IntensityService>();
        services.AddTransient<IIntervalService, IntervalService>();
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();