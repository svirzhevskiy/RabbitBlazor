using Newtonsoft.Json;

namespace RabbitBlazor.ViewModels;

public class Binding
{
    public Binding(string source, string destination, string destinationType, string routingKey, string propertiesKey)
    {
        Source = source;
        Destination = destination;
        DestinationType = destinationType;
        RoutingKey = routingKey;
        PropertiesKey = propertiesKey;
    }

    public string Source { get; set; }
    public string Destination { get; set; }
    public string DestinationType { get; set; }
    [JsonProperty("routing_key")]
    public string RoutingKey { get; set; }
    public string PropertiesKey { get; set; }
}