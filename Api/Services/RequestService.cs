using System.Globalization;
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
    private readonly IIntervalService _intervalService;

    public RequestService(IRequestsRepository requestsRepository, IIntervalService intervalService)
    {
        _requestsRepository = requestsRepository;
        _intervalService = intervalService;
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

    /// <inheritdoc cref="IRequestService.GenerateRequests"/>
    public IEnumerable<Request> GenerateRequests(int routesCount)
    {
        List<OneHourInterval> intervals = _intervalService.Intervals;
        var requests = new List<Request>();

        for (int hour = 1; hour < HoursInDay; hour++)
        {
            OneHourInterval interval = intervals[hour - 1];
            List<Request> requestsForInterval = GenerateRequestsForInterval(interval, routesCount).ToList();
            requests.AddRange(requestsForInterval);
        }

        return requests;
    }

    private IEnumerable<Request> GenerateRequestsForInterval(OneHourInterval interval, int routesCount)
    {
        var requestsForInterval = new List<Request>();
       
        int requestsCount = _random.Next(interval.MinRequestsCount * routesCount, 
            interval.MaxRequestsCount * routesCount);
        
        for (int i = 0; i < requestsCount; i++)
        {
            requestsForInterval.Add(CreateRequest(interval.StartHour, routesCount));
        }

        return requestsForInterval;
    }
    
    private Request CreateRequest(int hour, int routesCount)
    {
        var random = new Random();
        var request = new Request
        {
            RequestDateTime = new DateTime
                (DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hour, DateTime.Now.Minute, DateTime.Now.Second)
                .ToLocalTime().ToString(CultureInfo.InvariantCulture),
            SeatsCount = random.Next(MinSeatsCount, MaxSeatsCount),
            Route = new Route
            {
                Origin = random.Next(1, routesCount),
                Destination = random.Next(1, routesCount)
            }
        };

        return request;
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
    
    /// <inheritdoc cref="IRequestService.GenerateSummary"/>
    public IEnumerable<RequestsPerHourSummary> GenerateSummary(int routesCount)
    {
        List<Request> requests = GenerateRequests(routesCount).ToList();
        return GetRequestsSummary(requests);
    }

    /// <inheritdoc cref="IRequestService.GetRequestsSummary"/>
    public IEnumerable<RequestsPerHourSummary> GetRequestsSummary()
    {
        List<Request> allRequests = GetAllRequestsFromDatabase().ToList();
        return GetRequestsSummary(allRequests);
    }

    private IEnumerable<RequestsPerHourSummary> GetRequestsSummary(List<Request> requests)
    {
        var summaries = new List<RequestsPerHourSummary>();
        
        for (int hour = 1; hour <= HoursInDay; hour++)
        {
            RequestsPerHourSummary summary = GetSummaryForHour(requests, hour);
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