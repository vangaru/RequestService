using System.Globalization;

namespace RequestService.Common.Models;

/// <summary>
/// Represents transport request.
/// </summary>
public class Request
{
    /// <summary>
    /// Unique request identifier.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Count of seats for request.
    /// </summary>
    public int SeatsCount { get; set; }

    /// <summary>
    /// <see cref="Route" /> for that request.
    /// </summary>
    public Route? Route { get; set; }

    /// <summary>
    /// Request datetime.
    /// </summary>
    public string RequestDateTime { get; set; } = DateTime.Now.ToLocalTime().ToString(CultureInfo.InvariantCulture);

    public override string ToString()
    {
        return $"Id - {Id}; " +
               $"Seats - {SeatsCount}; " +
               $"Route - ({Route}); " +
               $"DateTime - {RequestDateTime}; ";
    }
}