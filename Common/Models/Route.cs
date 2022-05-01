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

    public override string ToString()
    {
        return $"Id - {Id}; " +
               $"Origin - {Origin}; " +
               $"Destination - {Destination}; ";
    }
}