namespace RequestService.Common.Configuration;

/// <summary>
/// Represents RabbitMQ configuration.
/// </summary>
public class RabbitMqConfiguration
{
    /// <summary>
    /// RabbitMQ queue name.
    /// </summary>
    public string? QueueName { get; set; }
    
    /// <summary>
    /// RabbitMQ host name.
    /// </summary>
    public string? HostName { get; set; }

    /// <summary>
    /// RabbitMQ virtual host.
    /// </summary>
    public string? VirtualHost { get; set; }

    /// <summary>
    /// RabbitMQ username.
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// RabbitMQ user's password.
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// RabbitMQ port.
    /// </summary>
    public int Port { get; set; }
}