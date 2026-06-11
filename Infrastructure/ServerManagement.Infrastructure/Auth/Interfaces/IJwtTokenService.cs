namespace ServerManagement.Infrastructure.Auth.Interfaces;

public interface IJwtTokenService
{
    /// <summary>
    /// Generate a JWT token for the given user
    /// </summary>
    /// <param name="user">The user for whom the JWT token needs to be generated</param>
    /// <returns>The generated JWT token and its expiry</returns>
    Task<AuthToken> GenerateJwtTokenAsync(ApplicationUser user);
}
