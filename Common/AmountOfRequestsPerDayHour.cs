namespace RequestService.Common;

/// <summary>
/// Represents amount of requests per hour of day.
/// </summary>
public class AmountOfRequestsPerDayHour
{
    /// <summary>
    /// Hour of day.
    /// </summary>
    public int HourOfDay { get; set; }

    /// <summary>
    /// Amount of requests.
    /// </summary>
    public int AmountOfRequests { get; set; }
}