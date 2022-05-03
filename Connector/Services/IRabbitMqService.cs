namespace RequestService.Connector.Services;

public interface IRabbitMqService : IDisposable
{
    /// <summary>
    /// Receives requests from API and sends them to message queue.
    /// </summary>
    public void ReceiveRequestsAndPopulateQueue();
}