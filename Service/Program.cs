using Microsoft.EntityFrameworkCore;
using RequestService;
using RequestService.Api.Services;
using RequestService.Common.Configuration;
using RequestService.Common.HttpClients;
using RequestService.Data;
using RequestService.Repositories;
using RequestService.Services;

const string defaultConnectionProp = "DefaultConnection";

await CreateHostBuilder(args).Build().RunAsync();

IHostBuilder CreateHostBuilder(string[] args)
{
    return Host.CreateDefaultBuilder(args)
        .ConfigureServices((hostContext, services) =>
        {
            IConfigurationSection requestsConfigurationSection =
                hostContext.Configuration.GetSection(nameof(RequestsConfiguration));
            var requestsConfiguration = requestsConfigurationSection.Get<RequestsConfiguration>();

            services.AddSingleton(requestsConfiguration);

            services.AddTransient<IIntensityService, IntensityService>();
            services.AddTransient<IIntervalService, IntervalService>();
            services.AddTransient<IDatabaseRepository, DatabaseRepository>();
            services.AddTransient<IRequestsHttpClient, RequestsHttpClient>();
            services.AddHostedService<Worker>();
            
            var optionsBuilder = new DbContextOptionsBuilder<RequestsContext>();
            optionsBuilder.UseSqlite(hostContext.Configuration.GetConnectionString(defaultConnectionProp));
            services.AddScoped(_ => new RequestsContext(optionsBuilder.Options));
        });
}