using ServerManagement.Models;

namespace ServerManagement.Clients;

public class ServerClient(HttpClient client)
{
    public async Task<List<Server>?> GetServers()
    {
        var servers = await client.GetAsync("http://localhost:3001/servers");
        return await servers.Content.ReadFromJsonAsync<List<Server>>();
    }
}
