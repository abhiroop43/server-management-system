namespace ServerManagement.Infrastructure.Auth.Interfaces;

public interface IJwtTokenService
{
    Task<string> GenerateJwtTokenAsync(ApplicationUser user);
}
