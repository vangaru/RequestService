﻿using System.Text.Json;
using RequestService.Api.Configuration;
using RequestService.Api.Services;
using RequestService.Common;

namespace RequestService.Services;

/// <summary>
/// Implementation of <see cref="IIntervalService"/>.
/// </summary>
public class IntervalService : IIntervalService
{
    private readonly RequestsConfiguration _requestsConfiguration;
    private readonly List<OneHourInterval> _intervals;

    /// <summary>
    /// Creates new instance of <see cref="IntervalService"/>.
    /// </summary>
    /// <param name="requestsConfiguration">Represents configuration of <see cref="RequestsConfiguration"/>.</param>
    public IntervalService(RequestsConfiguration requestsConfiguration)
    {
        _requestsConfiguration = requestsConfiguration;
        _intervals = ReadIntervalsFromConfig();
    }

    /// <inheritdoc cref="IIntervalService.CurrentInterval"/>
    public OneHourInterval CurrentInterval => _intervals.First(interval => interval.StartHour == DateTime.Now.Hour);

    private List<OneHourInterval> ReadIntervalsFromConfig()
    {
        string? intervalsFilePath = _requestsConfiguration.IntervalsFilePath;

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