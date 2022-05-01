using System.Text;
using System.Text.Json.Nodes;
using RequestService.Common.Configuration;

namespace RequestService.Common.HttpClients;

public class RequestsHttpClient : IRequestsHttpClient, IDisposable
{
    private const string RequestsControllerEndpoint = "requests";
    private const string JsonMediaType = "application/json";
    private const string RouteOriginKey = "origin";
    private const string RoutesCountKey = "routesCount";
    
    private readonly HttpClient _httpClient = new();
    private readonly Uri _requestsControllerUri;
    private readonly RequestsConfiguration _requestsConfiguration;

    public RequestsHttpClient(RequestsConfiguration requestsConfiguration)
    {
        _requestsConfiguration = requestsConfiguration;
        
        if (_requestsConfiguration.ApiBaseUrl == null)
        {
            throw new NullReferenceException("RequestService.Api configuration field is not specified.");
        }

        var apiBaseUri = new Uri(_requestsConfiguration.ApiBaseUrl);
        _requestsControllerUri = new Uri(apiBaseUri, RequestsControllerEndpoint);
    }

    public async Task SubmitRequestAsync(int origin)
    {
        var routeDataJsonObject = new JsonObject
        {
            [RouteOriginKey] = origin,
            [RoutesCountKey] = _requestsConfiguration.RoutesCount
        };

        var routeDataRequestContent = new StringContent(routeDataJsonObject.ToString(), Encoding.UTF8, JsonMediaType);
        await _httpClient.PostAsync(_requestsControllerUri, routeDataRequestContent);
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }
}