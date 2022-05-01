using System.Text;
using System.Text.Json.Nodes;
using RequestService.Common.Configuration;
using RequestService.Common.Models;

namespace RequestService.Common.HttpClients;

/// <summary>
/// Implementation of <see cref="IRequestsHttpClient"/>
/// </summary>
public class RequestsHttpClient : IRequestsHttpClient, IDisposable
{
    private const string RequestsControllerEndpoint = "requests/";
    private const string GetAllRequestsMethodEndpoint = "all/";
    private const string GetSummaryMethodEndpoint = "summary/";
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

    /// <inheritdoc cref="IRequestsHttpClient.SubmitRequestAsync"/>
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

    /// <inheritdoc cref="IRequestsHttpClient.GetAllRequestsAsync"/>
    public async Task<IEnumerable<Request>> GetAllRequestsAsync()
    {
        var getAllRequestsUri = new Uri(_requestsControllerUri, GetAllRequestsMethodEndpoint);
        HttpResponseMessage getAllRequestsContent = await _httpClient.GetAsync(getAllRequestsUri);
        var requests = await getAllRequestsContent.Content.ReadAsAsync<IEnumerable<Request>>();
        return requests;
    }

    /// <inheritdoc cref="IRequestsHttpClient.GetSummaryAsync"/>
    public async Task<IEnumerable<RequestsPerHourSummary>> GetSummaryAsync()
    {
        var getSummaryUri = new Uri(_requestsControllerUri, GetSummaryMethodEndpoint);
        HttpResponseMessage getSummaryContent = await _httpClient.GetAsync(getSummaryUri);
        var summary = await getSummaryContent.Content.ReadAsAsync<IEnumerable<RequestsPerHourSummary>>();
        return summary;
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }
}