using RequestService.Api.Repositories;
using RequestService.Common.Models;
using Route = RequestService.Common.Models.Route;

namespace RequestService.Api.Services;

/// <summary>
/// Implementation of <see cref="IRequestService"/>.
/// </summary>
public class RequestService : IRequestService
{
    private const int MinSeatsCount = 1;
    private const int MaxSeatsCount = 4;
    
    private readonly Random _random = new();
    private readonly IRequestsRepository _requestsRepository;

    public RequestService(IRequestsRepository requestsRepository)
    {
        _requestsRepository = requestsRepository;
    }
    
    /// <inheritdoc cref="IRequestService.GenerateAndSaveRequest"/>
    public void GenerateAndSaveRequest(Route route)
    {
        Request request = GenerateRequest(route);
        _requestsRepository.Add(request);
    }

    /// <inheritdoc cref="IRequestService.GenerateRequest"/>
    public Request GenerateRequest(Route route)
    {
        string requestId = Guid.NewGuid().ToString();
        
        var request = new Request
        {
            Id = requestId,
            Route = route,
            SeatsCount = SeatsCount
        };

        return request;
    }
    
    private int SeatsCount => _random.Next(MinSeatsCount, MaxSeatsCount);
}