namespace RabbitBlazor.ViewModels;

public class Exchange
{
    public Exchange(string name, bool durable, bool @internal, bool autoDelete, ExchangeType type)
    {
        Name = name;
        Durable = durable;
        Internal = @internal;
        AutoDelete = autoDelete;
        Type = type;
    }

    public string Name { get; }
    public bool Durable { get; }
    public bool Internal { get; }
    public bool AutoDelete { get; }
    public ExchangeType Type { get; }
}

public enum ExchangeType
{
    Direct,
    Topic,
    Headers,
    Fanout
}