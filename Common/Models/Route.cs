namespace RequestService.Common.Models;

/// <summary>
/// Route of particular request.
/// </summary>
public class Route
{
    /// <summary>
    /// Route unique identifier.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Route origin.
    /// </summary>
    public int Origin { get; set; }

    /// <summary>
    /// Route destination.
    /// </summary>
    public int Destination { get; set; }

    /// <summary>
    /// Id of the request corresponding to that route.
    /// </summary>
    public string? RequestId { get; set; }

    /// <summary>
    /// <see cref="Request" /> corresponding to that route.
    /// </summary>
    public Request? Request { get; set; }
}