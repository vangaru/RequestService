using System.Text.Json;
using RequestService.Common.Models;

namespace RequestService.Api.Services;

/// <summary>
/// Implementation of <see cref="IIntervalService"/>.
/// </summary>
public class IntervalService : IIntervalService
{
    private const string IntervalsFileKey = "IntervalsFilePath";
    
    private readonly IConfiguration _configuration;
    private readonly List<OneHourInterval> _intervals;

    /// <summary>
    /// Creates new instance of <see cref="IntervalService"/>.
    /// </summary>
    /// <param name="configuration">Configuration.</param>
    public IntervalService(IConfiguration configuration)
    {
        _configuration = configuration;
        _intervals = ReadIntervalsFromConfig();
    }

    /// <inheritdoc cref="IIntervalService.CurrentInterval"/>
    public OneHourInterval CurrentInterval => _intervals.First(interval => interval.StartHour == DateTime.Now.Hour);

    private List<OneHourInterval> ReadIntervalsFromConfig()
    {
        string intervalsFilePath = _configuration[IntervalsFileKey];

        if (intervalsFilePath == null)
        {
            throw new ApplicationException("Intervals file path cannot be null.");
        }

        string intervalsContent = File.ReadAllText(intervalsFilePath);
        var intervals = JsonSerializer.Deserialize<List<OneHourInterval>>(intervalsContent);
        
        if (intervals == null)
        {
            throw new ApplicationException($"Failed to deserialize contents of {intervalsFilePath}");
        }
        
        return intervals;
    }
}