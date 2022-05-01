using Microsoft.Extensions.Hosting;

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
            WriteToExcel(args, host);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args);
    }

    private static void WriteToExcel(string[] args, IHost host)
    {
        if (args.Length == 2)
        {
            string excelPath = args[1];
            Console.WriteLine($"Writing all records from db to {excelPath}...");
        }
        else if (args.Length == 3)
        {
            string excelPath = args[2];
            string recordingType = args[1];
            WriteWithRecordingType(excelPath, recordingType, host);
        }
        else
        {
            Console.WriteLine("Invalid number of arguments.");
        }
    }

    private static void WriteWithRecordingType(string excelPath, string recordingType, IHost host)
    {
        switch (recordingType)
        {
            case WriteAllArg:
                Console.WriteLine($"Writing all records from db to {excelPath}...");
                break;
            case WriteSummaryArg:
                Console.WriteLine($"Writing db records summary to {excelPath}...");
                break;
            case WriteGeneratedArg:
                Console.WriteLine($"Generating daily records and writing to {excelPath}...");
                break;
            default:
                Console.WriteLine($"Unrecognized parameter: {recordingType}. " +
                                  $"Available options are: {WriteAllArg}, {WriteSummaryArg}, {WriteGeneratedArg}");
                break;
        }
    }
}