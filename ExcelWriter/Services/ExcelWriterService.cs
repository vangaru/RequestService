using RequestService.Common.HttpClients;
using RequestService.Common.Models;
using RequestService.ExcelWriter.Helpers;

namespace RequestService.ExcelWriter.Services;

/// <summary>
/// Implementation of <see cref="IExcelWriterService"/>.
/// </summary>
public class ExcelWriterService : IExcelWriterService
{
    private readonly IRequestsHttpClient _httpClient;
    
    public ExcelWriterService(IRequestsHttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    /// <inheritdoc cref="IExcelWriterService.WriteRequestsFromDb"/>
    public void WriteRequestsFromDb(string excelPath)
    {
        IEnumerable<Request> requests = _httpClient.GetAllRequestsAsync().Result;
        ExcelWriterHelper.Write(requests, excelPath);
    }

    /// <inheritdoc cref="IExcelWriterService.WriteRequestsSummary"/>
    public void WriteRequestsSummary(string excelPath)
    {
        IEnumerable<RequestsPerHourSummary> summary = _httpClient.GetSummaryAsync().Result;
        ExcelWriterHelper.Write(summary, excelPath);
    }

    /// <inheritdoc cref="IExcelWriterService.WriteGeneratedRequests"/>
    public void WriteGeneratedRequests(string excelPath)
    {
        throw new NotImplementedException();
    }
}