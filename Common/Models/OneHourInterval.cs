namespace RequestService.Common.Models;

/// <summary>
/// One hour interval of time.
/// </summary>
public class OneHourInterval
{
    /// <summary>
    /// Start hour of the interval.
    /// </summary>
    public int StartHour { get; set; }

    /// <summary>
    /// Min count of requests during this interval.
    /// </summary>
    public int MinRequestsCount { get; set; }

    /// <summary>
    /// Max count of requests during this interval.
    /// </summary>
    public int MaxRequestsCount { get; set; }
}