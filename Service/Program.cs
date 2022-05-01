using RequestService;
using RequestService.Common.Configuration;
using RequestService.Common.HttpClients;
using RequestService.Services;

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
            services.AddTransient<IRequestsHttpClient, RequestsHttpClient>();
            services.AddHostedService<Worker>();
        });
}