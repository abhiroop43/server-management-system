namespace ServerManagement.UI.State.Interfaces;

/// <summary>
/// Interface for a token store that can be used to save and retrieve authentication tokens.
/// </summary>
public interface ITokenStore
{
    /// <summary>
    /// Gets the authentication token from the store.
    /// </summary>
    /// <returns>The authentication token, or null if not found.</returns>
    Task<string?> GetTokenAsync();

    /// <summary>
    /// Saves the authentication token to the store.
    /// </summary>
    /// <param name="token">The authentication token to save.</param>
    /// <returns></returns>
    Task SaveTokenAsync(string token);

    /// <summary>
    /// Clears the authentication token from the store.
    /// </summary>
    /// <returns></returns>
    Task ClearTokenAsync();
}
