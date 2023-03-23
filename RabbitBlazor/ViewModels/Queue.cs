namespace RabbitBlazor.ViewModels;

public class Queue
{
    public Queue(string name, bool durable, bool exclusive, bool autoDelete)
    {
        Name = name;
        Durable = durable;
        Exclusive = exclusive;
        AutoDelete = autoDelete;
    }

    public string Name { get; }
    public bool Durable { get; }
    public bool Exclusive { get; }
    public bool AutoDelete { get; }
}