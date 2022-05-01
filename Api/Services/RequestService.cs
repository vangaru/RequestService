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
    private const int HoursInDay = 24;
    
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

    /// <inheritdoc cref="IRequestService.GetAllRequestsFromDatabase"/>
    public IEnumerable<Request> GetAllRequestsFromDatabase()
    {
        return _requestsRepository.Get();
    }

    public IEnumerable<Request> GenerateRequests()
    {
        throw new NotImplementedException();
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

    /// <inheritdoc cref="IRequestService.GetRequestsSummary"/>
    public IEnumerable<RequestsPerHourSummary> GetRequestsSummary()
    {
        List<Request> allRequests = GetAllRequestsFromDatabase().ToList();
        
        var summaries = new List<RequestsPerHourSummary>();
        
        for (int hour = 1; hour <= HoursInDay; hour++)
        {
            RequestsPerHourSummary summary = GetSummaryForHour(allRequests, hour);
            summaries.Add(summary);
        }

        return summaries;
    }

    private RequestsPerHourSummary GetSummaryForHour(List<Request> requests, int hour)
    {
        int requestsCountForHour = requests.Count(request => DateTime.Parse(request.RequestDateTime).Hour == hour);
        var summary = new RequestsPerHourSummary
        {
            HourOfDay = hour,
            RequestsCount = requestsCountForHour
        };
        
        return summary;
    }

    private int SeatsCount => _random.Next(MinSeatsCount, MaxSeatsCount);
}