using ServerManagement.Models;

namespace ServerManagement.Clients;

public class ServerClient(HttpClient client, IConfiguration configuration)
{
    private readonly string _apiBaseUrl =
        configuration["ApiBaseUrl"]
        ?? throw new InvalidOperationException("Missing configuration: ApiBaseUrl");

    public async Task<List<ServerSummary>> GetServersList()
    {
        var response = await client.GetAsync($"{_apiBaseUrl}/servers");
        return await response.Content.ReadFromJsonAsync<List<ServerSummary>>() ?? [];
    }

    public async Task<Server?> GetServerDetails(Guid serverId)
    {
        var response = await client.GetAsync(
            $"{_apiBaseUrl}/servers/0a1b2c3d-4e5f-4789-a0b1-c2d3e4f50101"
        );
        return await response.Content.ReadFromJsonAsync<Server>();
    }
}
