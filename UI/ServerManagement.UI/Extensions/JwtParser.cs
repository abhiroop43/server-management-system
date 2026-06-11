namespace ServerManagement.UI.Extensions;

public static class JwtParser
{
    /// <summary>
    /// Parse the claims from the JWT token
    /// </summary>
    /// <param name="jwt">The JWT token</param>
    /// <returns>The claims from the JWT token</returns>
    public static IEnumerable<Claim> ParseClaims(string jwt)
    {
        var handler = new JsonWebTokenHandler();
        var token = handler.ReadJsonWebToken(jwt);
        return token.Claims;
    }
}
