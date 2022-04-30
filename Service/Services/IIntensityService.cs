namespace RequestService.Services;

/// <summary>
/// Calculates delay in milliseconds for the next execution step of worker service.
/// </summary>
public interface IIntensityService
{
    /// <summary>
    /// Gets delay of the next execution step according to the current time.
    /// </summary>
    /// <returns>
    /// Delay of the next execution step in millis.
    /// </returns>
    public int DelayInMillis { get; }
}