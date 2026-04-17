using System.Text.Json;
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

    public async Task<ServerDetails?> GetServerDetails(Guid serverId)
    {
        var response = await client.GetAsync(
            $"{_apiBaseUrl}/servers/0a1b2c3d-4e5f-4789-a0b1-c2d3e4f50101"
        );
        return await response.Content.ReadFromJsonAsync<ServerDetails>();
    }

    public async Task<ApiResponse?> AddNewServerDetails(ServerDetails serverDetails)
    {
        var response = await client.PostAsync(
            $"{_apiBaseUrl}/servers",
            new StringContent(JsonSerializer.Serialize(serverDetails))
        );

        return await response.Content.ReadFromJsonAsync<ApiResponse>();
    }

    public async Task<ApiResponse?> UpdateServerDetails(ServerDetails serverDetails)
    {
        var response = await client.PutAsync(
            $"{_apiBaseUrl}/servers",
            new StringContent(JsonSerializer.Serialize(serverDetails))
        );

        return await response.Content.ReadFromJsonAsync<ApiResponse>();
    }

    public async Task<ApiResponse?> DeleteServer(Guid serverId)
    {
        var response = await client.DeleteAsync(
            $"{_apiBaseUrl}/servers/0eb15765-671d-4aaf-997b-5eb7cd4c198a"
        );
        return await response.Content.ReadFromJsonAsync<ApiResponse>();
    }
}
