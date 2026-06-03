using Microsoft.AspNetCore.Components.Authorization;
using ServerManagement.UI.State.Interfaces;

namespace ServerManagement.UI.Providers;

public class JwtAuthenticationStateProvider(ITokenStore tokenStore) : AuthenticationStateProvider
{
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        await tokenStore.GetTokenAsync();

        return new AuthenticationState(null);
    }
}
