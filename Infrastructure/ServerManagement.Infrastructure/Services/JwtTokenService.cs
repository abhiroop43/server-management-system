using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using ServerManagement.Infrastructure.Auth.Interfaces;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace ServerManagement.Infrastructure.Services;

public class JwtTokenService(
    UserManager<ApplicationUser> userManager,
    IConfiguration configuration,
    IHttpContextAccessor httpContextAccessor
) : IJwtTokenService
{
    /// <summary>
    /// Generate a JWT token for the given user
    /// </summary>
    /// <param name="user">The user for whom the JWT token needs to be generated</param>
    /// <returns>The generated JWT token and its expiry</returns>
    public async Task<AuthToken> GenerateJwtTokenAsync(ApplicationUser user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var claims = await BuildClaimsAsync(user);

        var tokenExpiry = DateTime.Now.AddMinutes(
            Convert.ToDouble(configuration["Jwt:AccessTokenExpiryMinutes"])
        );

        var token = new SecurityTokenDescriptor
        {
            Issuer = configuration["Jwt:Issuer"],
            Audience = configuration["Jwt:Audience"],
            Claims = claims,
            Expires = tokenExpiry,
            SigningCredentials = credentials,
        };

        return new AuthToken(new JsonWebTokenHandler().CreateToken(token), tokenExpiry);
    }

    /// <summary>
    /// Validates the provided JWT token and retrieves the associated user.
    /// </summary>
    /// <returns>The user associated with the valid token, or will throw an exception if the token is invalid.</returns>
    public async Task<ApplicationUser> ValidateJwtTokenAsync()
    {
        var email = httpContextAccessor
            .HttpContext?.User?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)
            ?.Value;

        if (email == null)
            throw new UnauthorizedAccessException("Invalid token");
        var user = await userManager.FindByEmailAsync(email);

        return user ?? throw new UnauthorizedAccessException("Invalid token");
    }

    /// <summary>
    /// Build the claims for the JWT token
    /// </summary>
    /// <param name="user">The user for whom the JWT token needs to be generated</param>
    /// <returns>The claims for the JWT token</returns>
    private async Task<Dictionary<string, object>> BuildClaimsAsync(ApplicationUser user)
    {
        var claims = new Dictionary<string, object>
        {
            { JwtRegisteredClaimNames.Sub, user.Id },
            { JwtRegisteredClaimNames.Email, user.Email! },
            { JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString() },
            { ClaimTypes.NameIdentifier, user.Id },
            { ClaimTypes.GivenName, user.FirstName },
        };

        if (!string.IsNullOrEmpty(user.LastName))
            claims.Add(ClaimTypes.Surname, user.LastName);

        if (user.DateOfBirth != null)
            claims.Add(ClaimTypes.DateOfBirth, user.DateOfBirth.Value.ToString("yyyy-MM-dd"));

        var roles = await userManager.GetRolesAsync(user);

        foreach (var role in roles)
            claims.Add(ClaimTypes.Role, role);

        return claims;
    }
}
