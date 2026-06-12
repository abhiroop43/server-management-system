using System.Text.Json;
using ServerManagement.UI.Models;

namespace ServerManagement.UI.Clients;

public class AuthClient(HttpClient client, IConfiguration configuration)
{
    private readonly string _apiBaseUrl =
        configuration["ApiBaseUrl"]
        ?? throw new InvalidOperationException("Missing configuration: ApiBaseUrl");

    public async Task<LoginResponse?> Login(UserLogin loginRequest)
    {
        var response = await client.PostAsync(
            $"{_apiBaseUrl}/auth/login",
            new StringContent(JsonSerializer.Serialize(loginRequest))
        );

        return await response.Content.ReadFromJsonAsync<LoginResponse>();
    }

    public async Task<RegisterResponse?> Register(UserRegistration registerRequest)
    {
        var response = await client.PostAsync(
            $"{_apiBaseUrl}/auth/register",
            new StringContent(JsonSerializer.Serialize(registerRequest))
        );

        return await response.Content.ReadFromJsonAsync<RegisterResponse>();
    }
}
