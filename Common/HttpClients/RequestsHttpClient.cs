using System.Text;
using System.Text.Json.Nodes;
using RequestService.Common.Configuration;
using RequestService.Common.Models;

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

    public async Task<Request> GetRequestAsync(int origin)
    {
        var routeDataJsonObject = new JsonObject
        {
            [RouteOriginKey] = origin,
            [RoutesCountKey] = _requestsConfiguration.RoutesCount
        };

        var routeDataRequestContent = new StringContent(routeDataJsonObject.ToString(), Encoding.UTF8, JsonMediaType);
        
        HttpResponseMessage response = await _httpClient.PostAsync(_requestsControllerUri, routeDataRequestContent);
        var request = await response.Content.ReadAsAsync<Request>();
        return request;
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }
}