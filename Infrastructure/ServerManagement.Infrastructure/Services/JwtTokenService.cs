using ServerManagement.Infrastructure.Auth.Interfaces;

namespace ServerManagement.Infrastructure.Services;

public class JwtTokenService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
    : IJwtTokenService
{
    public async Task<AuthToken> GenerateJwtTokenAsync(ApplicationUser user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var claims = await BuildClaimsAsync(user);

        var tokenExpiry = DateTime.Now.AddMinutes(
            Convert.ToDouble(configuration["Jwt:AccessTokenExpiryMinutes"])
        );

        var token = new JwtSecurityToken(
            configuration["Jwt:Issuer"],
            configuration["Jwt:Audience"],
            claims,
            expires: tokenExpiry,
            signingCredentials: credentials
        );

        return new AuthToken(new JwtSecurityTokenHandler().WriteToken(token), tokenExpiry);
    }

    private async Task<List<Claim>> BuildClaimsAsync(ApplicationUser user)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Email, user.Email!),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.GivenName, user.FirstName),
        };

        if (!string.IsNullOrEmpty(user.LastName))
        {
            claims.Add(new(ClaimTypes.Surname, user.LastName));
        }

        if (user.DateOfBirth != null)
        {
            claims.Add(new(ClaimTypes.DateOfBirth, user.DateOfBirth.Value.ToString("yyyy-MM-dd")));
        }

        var roles = await userManager.GetRolesAsync(user);

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        return claims;
    }
}
