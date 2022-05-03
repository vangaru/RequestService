using Newtonsoft.Json;
using RequestService.Common.HttpClients;
using RequestService.Common.Models;
using RequestService.Connector.Repositories;

namespace RequestService.Connector.Services;

/// <summary>
/// Implementation of <see cref="IRabbitMqService"/>
/// </summary>
public class RabbitMqService : IRabbitMqService
{
    private readonly IRabbitMqRepository _rabbitMqRepository;
    private readonly IRequestsHttpClient _httpClient;
    
    public RabbitMqService(IRabbitMqRepository rabbitMqRepository, IRequestsHttpClient httpClient)
    {
        _rabbitMqRepository = rabbitMqRepository;
        _httpClient = httpClient;
    }

    /// <inheritdoc cref="IRabbitMqService.ReceiveRequestsAndPopulateQueue"/>
    public void ReceiveRequestsAndPopulateQueue()
    {
        IEnumerable<Request> requests = _httpClient.GetGeneratedRequestsAsync().Result;
        foreach (Request request in requests)
        {
            string requestJson = SerializeRequest(request);
            _rabbitMqRepository.Enqueue(requestJson);
        }
    }

    private string SerializeRequest(Request request)
    {
        string requestJson = JsonConvert.SerializeObject(request);
        return requestJson;
    }
    
    public void Dispose()
    {
        _rabbitMqRepository.Dispose();
    }
}