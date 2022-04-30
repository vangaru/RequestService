namespace RequestService.Common;

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
    /// Id of the route for that request.
    /// </summary>
    public string? RouteId { get; set; }

    /// <summary>
    /// <see cref="Route" /> for that request.
    /// </summary>
    public Route? Route { get; set; }
}