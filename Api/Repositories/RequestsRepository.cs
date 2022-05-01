using Microsoft.EntityFrameworkCore;
using RequestService.Api.Data;
using RequestService.Common.Models;

namespace RequestService.Api.Repositories;

/// <summary>
/// Implements <see cref="IRequestsRepository"/>. Should be disposed after usage.
/// </summary>
public class RequestsRepository : IRequestsRepository
{
    private readonly RequestsContext _context;

    /// <summary>
    /// Creates new instance of <see cref="RequestsRepository"/>.
    /// </summary>
    /// <param name="context"><see cref="RequestsContext"/> instance to access database.</param>
    public RequestsRepository(RequestsContext context)
    {
        _context = context;
    }

    /// <inheritdoc cref="IRequestsRepository.Add"/>
    public void Add(Request request)
    {
        _context.Requests!.Add(request);
        _context.SaveChanges();
    }

    /// <inheritdoc cref="IRequestsRepository.AddAsync"/>
    public async Task AddAsync(Request request)
    {
        await _context.Requests!.AddAsync(request);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc cref="IRequestsRepository.AddRangeAsync"/>
    public async Task AddRangeAsync(IEnumerable<Request> requests)
    {
        await _context.Requests!.AddRangeAsync(requests);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc cref="IRequestsRepository.AddRange"/>
    public void AddRange(IEnumerable<Request> requests)
    {
        _context.Requests!.AddRange(requests);
        _context.SaveChanges();
    }

    /// <inheritdoc cref="IRequestsRepository.Add"/>
    public IEnumerable<Request> Get()
    {
        return _context.Requests!.Include(r => r.Route);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}