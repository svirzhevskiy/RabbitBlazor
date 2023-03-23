using Microsoft.Extensions.Options;
using RabbitBlazor.Common;
using RabbitMQ.Client;

namespace RabbitBlazor.Services.Base;

public abstract class RabbitMqClientBase : IDisposable
{
    protected IModel Channel { get; private set; }
    private IConnection _connection;
    private readonly ConnectionFactory _connectionFactory;
    protected RabbitMqHttpClient RabbitMqHttpClient { get; } 

    protected RabbitMqClientBase(ConnectionFactory connectionFactory, IOptions<RabbitMqSettings> rabbitMqSettings)
    {
        _connectionFactory = connectionFactory;
        RabbitMqHttpClient = new RabbitMqHttpClient(rabbitMqSettings);
        ConnectToRabbitMq();
    }

    protected void ConnectToRabbitMq()
    {
        if (_connection == null || _connection.IsOpen == false)
        {
            _connection = _connectionFactory.CreateConnection();
        }

        if (Channel == null || Channel.IsOpen == false)
        {
            Channel = _connection.CreateModel();
        }
    }

    public void Dispose()
    {
        try
        {
            Channel?.Close();
            Channel?.Dispose();
            Channel = null;

            _connection?.Close();
            _connection?.Dispose();
            _connection = null;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}