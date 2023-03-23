using System.Text;
using Microsoft.Extensions.Options;
using RabbitBlazor.Common;
using RabbitBlazor.Services.Base;
using RabbitMQ.Client;

namespace RabbitBlazor.Services;

public class MessagingService : RabbitMqClientBase
{
    public MessagingService(ConnectionFactory connectionFactory, IOptions<RabbitMqSettings> rabbitMqSettings) 
        : base(connectionFactory, rabbitMqSettings) { }

    public void PublishMessage(string exchange, string routingKey, string message)
    {
        Channel.BasicPublish(exchange, routingKey, null, Encoding.UTF8.GetBytes(message));
    }

    public IEnumerable<string> GetMessages(string routingKey, bool peek)
    {
        var queueMessages = new List<BasicGetResult>();
        while (Channel.BasicGet(routingKey, !peek) is {} getResult)
        {
            queueMessages.Add(getResult);
        }

        var result = new List<string>();
        foreach (var message in queueMessages)
        {
            result.Add(Encoding.UTF8.GetString(message.Body.ToArray()));
            if (peek)
            {
                Channel.BasicNack(message.DeliveryTag, true, true);
            }
        }
        
        return result;
    }
}