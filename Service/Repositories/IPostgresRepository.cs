using RequestService.Common;
using RequestService.Common.Models;

namespace RequestService.Repositories;

/// <summary>
/// Abstraction for accessing postgres database containing requests.
/// </summary>
public interface IPostgresRepository
{
    /// <summary>
    /// Adds <see cref="Request"/> to postgres database.
    /// </summary>
    /// <param name="request">Instance of <see cref="Request"/> to add to database.</param>
    public void Add(Request request);
    
    /// <summary>
    /// Adds <see cref="Request"/> to postgres database.
    /// </summary>
    /// <param name="request">Instance of <see cref="Request"/> to add to database.</param>
    public Task AddAsync(Request request);
    
    /// <summary>
    /// Gets all <see cref="Request"/> instances from database.
    /// </summary>
    /// <returns>All <see cref="Request"/> instances from database.</returns>
    public IEnumerable<Request> Get();
}