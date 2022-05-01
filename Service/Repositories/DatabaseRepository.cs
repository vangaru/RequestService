using Microsoft.EntityFrameworkCore;
using RequestService.Common.Models;
using RequestService.Data;

namespace RequestService.Repositories;

/// <summary>
/// Implements <see cref="IDatabaseRepository"/>. Should be disposed after usage.
/// </summary>
public class DatabaseRepository : IDatabaseRepository
{
    private readonly RequestsContext _context;

    /// <summary>
    /// Creates new instance of <see cref="DatabaseRepository"/>.
    /// </summary>
    /// <param name="context"><see cref="RequestsContext"/> instance to access database.</param>
    public DatabaseRepository(RequestsContext context)
    {
        _context = context;
    }

    /// <inheritdoc cref="IDatabaseRepository.Add"/>
    public void Add(Request request)
    {
        _context.Requests!.Add(request);
        _context.SaveChanges();
    }

    /// <inheritdoc cref="IDatabaseRepository.AddAsync"/>
    public async Task AddAsync(Request request)
    {
        await _context.Requests!.AddAsync(request);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc cref="IDatabaseRepository.AddRangeAsync"/>
    public async Task AddRangeAsync(IEnumerable<Request> requests)
    {
        await _context.Requests!.AddRangeAsync(requests);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc cref="IDatabaseRepository.AddRange"/>
    public void AddRange(IEnumerable<Request> requests)
    {
        _context.Requests!.AddRange(requests);
        _context.SaveChanges();
    }

    /// <inheritdoc cref="IDatabaseRepository.Add"/>
    public IEnumerable<Request> Get()
    {
        return _context.Requests!.Include(r => r.Route);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}