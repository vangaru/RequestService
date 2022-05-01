using RequestService.Common;

namespace RequestService.Api.Services;

/// <summary>
/// Implementation of <see cref="IRequestService"/>.
/// </summary>
public class RequestService : IRequestService
{
    private const int MinSeatsCount = 1;
    private const int MaxSeatsCount = 4;
    
    private readonly Random _random = new();
    
    /// <inheritdoc cref="IRequestService.GenerateRequest"/>
    public Request GenerateRequest(Common.Route route)
    {
        string requestId = Guid.NewGuid().ToString();
        
        var request = new Request
        {
            Id = requestId,
            RouteId = route.Id,
            Route = route,
            SeatsCount = SeatsCount
        };

        return request;
    }

    private int SeatsCount => _random.Next(MinSeatsCount, MaxSeatsCount);
}