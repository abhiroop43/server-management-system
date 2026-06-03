using ServerManagement.UI.State.Interfaces;

namespace ServerManagement.UI.State;

public class TokenStore : ITokenStore
{
    public Task ClearTokenAsync()
    {
        throw new NotImplementedException();
    }

    public Task<string?> GetTokenAsync()
    {
        throw new NotImplementedException();
    }

    public Task SaveTokenAsync(string token)
    {
        throw new NotImplementedException();
    }
}
