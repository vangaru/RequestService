using RequestService.Api.Services;
using RequestService.Common.Models;

namespace RequestService.Services;

/// <summary>
/// Implementation of <see cref="IIntensityService"/>
/// </summary>
public class IntensityService : IIntensityService
{
    private const int MillisInHour = 3_600_000;
    private const int DefaultInterval = 10_000;

    private readonly IIntervalService _intervalService;

    /// <summary>
    /// Creates new instance of <see cref="IntensityService"/>.
    /// </summary>
    /// <param name="intervalService"><see cref="IIntervalService"/>.</param>
    public IntensityService(IIntervalService intervalService)
    {
        _intervalService = intervalService;
    }

    public int DelayInMillis
    {
        get
        {
            OneHourInterval interval = _intervalService.CurrentInterval;
            var random = new Random();
            int requestsCount = random.Next(interval.MinRequestsCount, interval.MaxRequestsCount);
            return GetDelay(requestsCount);
        }
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