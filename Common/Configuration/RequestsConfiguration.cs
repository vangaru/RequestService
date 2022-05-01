namespace RequestService.Common.Configuration;

/// <summary>
/// Represents requests configuration.
/// </summary>
public class RequestsConfiguration
{
    /// <summary>
    /// Amount of accessible routes for request.
    /// </summary>
    public int RoutesCount { get; set; }
    
    /// <summary>
    /// Base Url of RequestService.Api.
    /// </summary>
    public string? ApiBaseUrl { get; set; }
}