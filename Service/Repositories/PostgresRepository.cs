﻿using Microsoft.EntityFrameworkCore;
using RequestService.Common;
using RequestService.Data;

namespace RequestService.Repositories;

/// <summary>
/// Implements <see cref="IPostgresRepository"/>. Should be disposed after usage.
/// </summary>
public class PostgresRepository : IPostgresRepository, IDisposable
{
    private readonly RequestsContext _context;

    /// <summary>
    /// Creates new instance of <see cref="PostgresRepository"/>.
    /// </summary>
    /// <param name="context"><see cref="RequestsContext"/> instance to access database.</param>
    public PostgresRepository(RequestsContext context)
    {
        _context = context;
    }

    /// <inheritdoc cref="IPostgresRepository.Add"/>
    public void Add(Request request)
    {
        _context.Requests!.Add(request);
        _context.SaveChanges();
    }

    /// <inheritdoc cref="IPostgresRepository.AddAsync"/>
    public async Task AddAsync(Request request)
    {
        await _context.Requests!.AddAsync(request);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc cref="IPostgresRepository.Add"/>
    public IEnumerable<Request> Get()
    {
        return _context.Requests!.Include(r => r.Route);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}