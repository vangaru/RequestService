using RequestService.Common.Configuration;
using RequestService.Common.Models;

namespace RequestService.Common.HttpClients;

public class IntervalsHttpClient : IIntervalsHttpClient, IDisposable
{
    private const string IntervalsControllerEndpoint = "intervals/";
    
    private readonly HttpClient _httpClient = new();
    private readonly Uri _intervalsControllerUri;

    public IntervalsHttpClient(RequestsConfiguration requestsConfiguration)
    {
        if (requestsConfiguration.ApiBaseUrl == null)
        {
            throw new NullReferenceException("RequestService.Api configuration field is not specified.");
        }
        
        var apiBaseUri = new Uri(requestsConfiguration.ApiBaseUrl);
        _intervalsControllerUri = new Uri(apiBaseUri, IntervalsControllerEndpoint);
    }
    
    public async Task<OneHourInterval> GetCurrentIntervalAsync()
    {
        HttpResponseMessage getCurrentIntervalContent = await _httpClient.GetAsync(_intervalsControllerUri);
        var currentInterval = await getCurrentIntervalContent.Content.ReadAsAsync<OneHourInterval>();
        return currentInterval;
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }
}