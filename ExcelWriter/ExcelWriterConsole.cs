using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RequestService.Common.Configuration;
using RequestService.Common.HttpClients;
using RequestService.ExcelWriter.Services;

namespace RequestService.ExcelWriter;

public static class ExcelWriterConsole
{
    private const string WriteAllArg = "--all";
    private const string WriteSummaryArg = "--summary";
    private const string WriteGeneratedArg = "--generated";
    
    public static void Write(string[] args)
    {
        if (args.Length <= 1)
        {
            Console.WriteLine("Please, specify required parameters.");
            return;
        }
        try
        {
            using IHost host = CreateHostBuilder(args).Build();
            var excelWriterService = host.Services.GetRequiredService<IExcelWriterService>();
            WriteToExcel(args, excelWriterService);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddTransient<IExcelWriterService, ExcelWriterService>();
                services.AddTransient<IRequestsHttpClient, RequestsHttpClient>();
                
                IConfigurationSection requestsConfigurationSection =
                    hostContext.Configuration.GetSection(nameof(RequestsConfiguration));
                var requestsConfiguration = requestsConfigurationSection.Get<RequestsConfiguration>();
                
                services.AddSingleton(requestsConfiguration);
                
            });
    }

    private static void WriteToExcel(string[] args, IExcelWriterService excelWriterService)
    {
        if (args.Length == 2)
        {
            string excelPath = args[1];
            Console.WriteLine($"Writing all records from db to {excelPath}...");
            excelWriterService.WriteRequestsFromDb(excelPath);
        }
        else if (args.Length == 3)
        {
            string excelPath = args[2];
            string recordingType = args[1];
            WriteWithRecordingType(excelPath, recordingType, excelWriterService);
        }
        else
        {
            Console.WriteLine("Invalid number of arguments.");
        }
    }

    private static void WriteWithRecordingType(string excelPath, string recordingType, IExcelWriterService excelWriterService)
    {
        switch (recordingType)
        {
            case WriteAllArg:
                Console.WriteLine($"Writing all records from db to {excelPath}...");
                excelWriterService.WriteRequestsFromDb(excelPath);
                break;
            case WriteSummaryArg:
                Console.WriteLine($"Writing db records summary to {excelPath}...");
                excelWriterService.WriteRequestsSummary(excelPath);
                break;
            case WriteGeneratedArg:
                Console.WriteLine($"Generating daily records and writing to {excelPath}...");
                excelWriterService.WriteGeneratedSummary(excelPath);
                break;
            default:
                Console.WriteLine($"Unrecognized parameter: {recordingType}. " +
                                  $"Available options are: {WriteAllArg}, {WriteSummaryArg}, {WriteGeneratedArg}");
                break;
        }
    }
}