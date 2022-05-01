using RequestService.Common.HttpClients;
using RequestService.Common.Models;

namespace RequestService.Services;

/// <summary>
/// Implementation of <see cref="IIntensityService"/>
/// </summary>
public class IntensityService : IIntensityService
{
    private const int MillisInHour = 3_600_000;
    private const int DefaultInterval = 10_000;

    private readonly IIntervalsHttpClient _httpClient;

    public IntensityService(IIntervalsHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public int GetDelayInMillis()
    {
        OneHourInterval interval = _httpClient.GetCurrentIntervalAsync().Result;
        var random = new Random();
        int requestsCount = random.Next(interval.MinRequestsCount, interval.MaxRequestsCount);
        return GetDelay(requestsCount);
    }

    private int GetDelay(int requestsCount)
    {
        try
        {
            return MillisInHour / requestsCount;
        }
        catch (DivideByZeroException)
        {
            return DefaultInterval;
        }
    }
}