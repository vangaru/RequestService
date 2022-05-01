using RequestService.Common.Models;

namespace RequestService.Common.HttpClients;

/// <summary>
/// Provides methods to access IntervalsController API methods.
/// </summary>
public interface IIntervalsHttpClient
{
    /// <summary>
    /// Calls Api GetCurrentInterval method.
    /// </summary>
    /// <returns></returns>
    public Task<OneHourInterval> GetCurrentIntervalAsync();
}