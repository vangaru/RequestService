using System.Text;
using RabbitMQ.Client;
using RequestService.Common.Configuration;

namespace RequestService.Connector.Repositories;

/// <summary>
/// Implementation of <see cref="IRabbitMqRepository"/>
/// </summary>
public class RabbitMqRepository : IRabbitMqRepository
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly RabbitMqConfiguration _rabbitMqConfiguration;

    public RabbitMqRepository(RabbitMqConfiguration rabbitMqConfiguration)
    {
        _rabbitMqConfiguration = rabbitMqConfiguration;
        var factory = new ConnectionFactory
        {
            HostName = _rabbitMqConfiguration.HostName,
            UserName = _rabbitMqConfiguration.UserName,
            Password = _rabbitMqConfiguration.Password,
            Port = _rabbitMqConfiguration.Port,
            VirtualHost = _rabbitMqConfiguration.VirtualHost
        };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
    }
    
    /// <inheritdoc cref="IRabbitMqRepository.Enqueue"/>
    public void Enqueue(string contents)
    {
        _channel.QueueDeclare(
            queue: _rabbitMqConfiguration.QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        byte[] messageBody = Encoding.UTF8.GetBytes(contents);
        
        _channel.BasicPublish(
            exchange: "",
            routingKey: _rabbitMqConfiguration.QueueName,
            basicProperties: null,
            body: messageBody);
    }

    public void Dispose()
    {
        _connection.Dispose();
        _channel.Dispose();
    }
}