namespace RabbitBlazor.Common;

public static class Helper
{
    public static string CreateRoutingKey(string exchange, string queue) => $"{exchange}-{queue}";
}