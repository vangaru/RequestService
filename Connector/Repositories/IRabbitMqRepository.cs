namespace RequestService.Connector.Repositories;

/// <summary>
/// Provides methods to access RabbitMQ.
/// </summary>
public interface IRabbitMqRepository : IDisposable
{
    /// <summary>
    /// Adds message to message queue.
    /// </summary>
    /// <param name="contents">Message to add.</param>
    public void Enqueue(string contents);
}