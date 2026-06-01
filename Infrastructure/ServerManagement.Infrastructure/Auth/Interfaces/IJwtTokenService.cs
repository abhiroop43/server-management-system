namespace ServerManagement.Infrastructure.Auth.Interfaces;

public interface IJwtTokenService
{
    Task<AuthToken> GenerateJwtTokenAsync(ApplicationUser user);
}
