namespace RequestService.Common.Models;

/// <summary>
/// Represents summary about amount of requests per hour.
/// </summary>
public class RequestsPerHourSummary
{
    /// <summary>
    /// Gets or sets hour of day.
    /// </summary>
    public int HourOfDay { get; set; }
    
    /// <summary>
    /// Gets or sets requests count.
    /// </summary>
    public int RequestsCount { get; set; }
}