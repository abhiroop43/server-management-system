namespace ServerManagement.UI.Providers;

public class JwtAuthenticationStateProvider(ITokenStore tokenStore) : AuthenticationStateProvider
{
    /// <summary>
    /// Gets the authentication state.
    /// </summary>
    /// <returns>The user details or blank claims</returns>
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await tokenStore.GetTokenAsync();

        if (string.IsNullOrWhiteSpace(token))
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        var claims = JwtParser.ParseClaims(token);
        var identity = new ClaimsIdentity(claims, "jwt");
        var user = new ClaimsPrincipal(identity);

        return new AuthenticationState(user);
    }

    /// <summary>
    /// Notifies the authentication state provider that the authentication state has changed.
    /// </summary>
    /// <param name="token">The authentication token</param>
    public async Task NotifyAuthenticationAsync(string token)
    {
        await tokenStore.SaveTokenAsync(token);

        var claims = JwtParser.ParseClaims(token);
        var identity = new ClaimsIdentity(claims, "jwt");
        var user = new ClaimsPrincipal(identity);

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    /// <summary>
    /// Clears the token and notifies the authentication state provider that the authentication state has changed.
    /// </summary>
    public async Task NotifyUserLogoutAsync()
    {
        await tokenStore.ClearTokenAsync();
        var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymous)));
    }
}
