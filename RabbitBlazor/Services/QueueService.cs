using Microsoft.Extensions.Options;
using RabbitBlazor.Common;
using RabbitBlazor.Services.Base;
using RabbitBlazor.ViewModels;
using RabbitMQ.Client;

namespace RabbitBlazor.Services;

public class QueueService : RabbitMqClientBase
{

    public QueueService(ConnectionFactory connectionFactory, IOptions<RabbitMqSettings> rabbitMqSettings) 
        : base(connectionFactory, rabbitMqSettings) { }
    
    public void Create(Queue queue)
    {
        Channel.QueueDeclare(queue.Name, queue.Durable, queue.Exclusive, queue.AutoDelete);
    }

    public void Delete(string queueName, bool isUnused = false, bool isEmpty = false)
    {
        Channel.QueueDelete(queueName, isUnused, isEmpty);
    }

    public Task<IEnumerable<Queue>> GetQueues()
    {
        return RabbitMqHttpClient.GetQueues();
    }
}