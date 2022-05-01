using RequestService.Common.Models;

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
    
    /// <summary>
    /// Reads all intervals from config.
    /// </summary>
    public List<OneHourInterval> Intervals { get; }
}