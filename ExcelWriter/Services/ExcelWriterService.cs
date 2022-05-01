using RequestService.Common.HttpClients;

namespace RequestService.ExcelWriter.Services;

public class ExcelWriterService : IExcelWriterService
{
    private readonly IRequestsHttpClient _httpClient;
    
    public ExcelWriterService(IRequestsHttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public void WriteRequestsFromDb(string excelPath)
    {
        throw new NotImplementedException();
    }

    public void WriteRequestsSummary(string excelPath)
    {
        throw new NotImplementedException();
    }

    public void WriteGeneratedRequests(string excelPath)
    {
        throw new NotImplementedException();
    }
}