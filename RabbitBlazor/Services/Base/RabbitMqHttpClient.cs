using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitBlazor.Common;
using RabbitBlazor.ViewModels;

namespace RabbitBlazor.Services.Base;

public class RabbitMqHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly string _authenticationString;

    public RabbitMqHttpClient(IOptions<RabbitMqSettings> rabbitMqSettings)
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(rabbitMqSettings.Value.RabbitMqUrl);

        _authenticationString =
            Convert.ToBase64String(
                System.Text.ASCIIEncoding.ASCII.GetBytes(
                    $"{rabbitMqSettings.Value.User}:{rabbitMqSettings.Value.Password}"));
    }

    private async Task<T?> SendGetRequest<T>(string url)
    {
        try
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", _authenticationString);

            var response = await _httpClient.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<T>(responseContent, new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore
            });

            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return default;
        }
    }

    public async Task<IEnumerable<Queue>> GetQueues()
    {
        var result = await SendGetRequest<IEnumerable<Queue>>("api/queues");
        return result ?? new List<Queue>();
    }
    
    public async Task<IEnumerable<Exchange>> GetExchanges()
    {
        var result = await SendGetRequest<IEnumerable<Exchange>>("api/exchanges");
        return result ?? new List<Exchange>();
    }

    public async Task<IEnumerable<Binding>> GetBindings()
    {
        var result = await SendGetRequest<IEnumerable<Binding>>("api/bindings");
        return result ?? new List<Binding>();
    }
}