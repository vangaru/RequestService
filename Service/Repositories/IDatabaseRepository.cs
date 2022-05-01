using RequestService.Common.Models;

namespace RequestService.Repositories;

/// <summary>
/// Abstraction for accessing postgres database containing requests.
/// </summary>
public interface IDatabaseRepository : IDisposable
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
    /// Adds range of <see cref="Request"/> to postgres database.
    /// </summary>
    /// <param name="requests">Instances of <see cref="Request"/> to add to database.</param>
    public void AddRange(IEnumerable<Request> requests);

    /// <summary>
    /// Adds range of <see cref="Request"/> to postgres database.
    /// </summary>
    /// <param name="requests">Instances of <see cref="Request"/> to add to database.</param>
    public Task AddRangeAsync(IEnumerable<Request> requests);
    
    /// <summary>
    /// Gets all <see cref="Request"/> instances from database.
    /// </summary>
    /// <returns>All <see cref="Request"/> instances from database.</returns>
    public IEnumerable<Request> Get();
}