using RequestService.Common;

namespace RequestService.Api.Services;

/// <summary>
/// Provides <see cref="OneHourInterval"/> according to the current time.
/// </summary>
public interface IIntervalService
{
    /// <summary>
    /// Provides <see cref="OneHourInterval"/> according to the current time.
    /// </summary>
    /// <returns>
    /// <see cref="OneHourInterval"/> instance.
    /// </returns>
    public OneHourInterval CurrentInterval { get; }
}