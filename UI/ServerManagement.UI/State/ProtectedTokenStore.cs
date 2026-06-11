using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace ServerManagement.UI.State;

public class ProtectedTokenStore(ProtectedLocalStorage protectedLocalStorage) : ITokenStore
{
    private const string TokenKey = "authToken";

    /// <summary>
    /// Clears the authentication token from the store.
    /// </summary>
    /// <returns></returns>
    public async Task ClearTokenAsync()
    {
        await protectedLocalStorage.DeleteAsync(TokenKey);
    }

    /// <summary>
    /// Gets the authentication token from the store.
    /// </summary>
    /// <returns>The authentication token, or null if not found.</returns>
    public async Task<string?> GetTokenAsync()
    {
        var result = await protectedLocalStorage.GetAsync<string>(TokenKey);
        return result.Success ? result.Value : null;
    }

    /// <summary>
    /// Saves the authentication token to the store.
    /// </summary>
    /// <param name="token">The authentication token to save.</param>
    /// <returns></returns>
    public async Task SaveTokenAsync(string token)
    {
        await protectedLocalStorage.SetAsync(TokenKey, token);
    }
}
