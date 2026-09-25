namespace ServerManagement.Infrastructure.Auth.Interfaces;

public interface IJwtTokenService
{
    /// <summary>
    /// Generate a JWT token for the given user
    /// </summary>
    /// <param name="user">The user for whom the JWT token needs to be generated</param>
    /// <returns>The generated JWT token and its expiry</returns>
    Task<AuthToken> GenerateJwtTokenAsync(ApplicationUser user);

    /// <summary>
    /// Validates the provided JWT token and retrieves the associated user.
    /// </summary>
    /// <returns>The user associated with the valid token, or will throw an exception if the token is invalid.</returns>
    Task<ApplicationUser> ValidateJwtTokenAsync();
}
