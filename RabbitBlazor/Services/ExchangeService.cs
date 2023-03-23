using Microsoft.Extensions.Options;
using RabbitBlazor.Common;
using RabbitBlazor.Services.Base;
using RabbitBlazor.ViewModels;
using RabbitMQ.Client;

namespace RabbitBlazor.Services;

public class ExchangeService : RabbitMqClientBase
{
    public ExchangeService(ConnectionFactory connectionFactory, IOptions<RabbitMqSettings> rabbitMqSettings) 
        : base(connectionFactory, rabbitMqSettings) { }

    public Task<IEnumerable<Exchange>> GetExchanges()
    {
        return RabbitMqHttpClient.GetExchanges();
    }
    
    public void Create(Exchange exchange)
    {
        Channel.ExchangeDeclare(exchange.Name, exchange.Type.ToString().ToLower(), exchange.Durable, exchange.AutoDelete);
    }

    public void Bind(string exchange, string queue)
    {
        Channel.QueueBind(queue, exchange, Helper.CreateRoutingKey(exchange, queue));
    }

    public async Task<IEnumerable<Binding>> GetBindings(string exchange = null, string queue = null)
    {
        var bindings = await RabbitMqHttpClient.GetBindings();

        if (exchange != null)
        {
            bindings = bindings.Where(x => x.Source == exchange);
        }

        if (queue != null)
        {
            bindings = bindings.Where(x => x.Destination == queue);
        }

        return bindings;
    }
}