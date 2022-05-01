using RequestService.Common.HttpClients;
using RequestService.Common.Models;
using RequestService.ExcelWriter.Helpers;

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
        IEnumerable<Request> requests = _httpClient.GetAllRequestsAsync().Result;
        ExcelWriterHelper.Write(requests, excelPath);
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