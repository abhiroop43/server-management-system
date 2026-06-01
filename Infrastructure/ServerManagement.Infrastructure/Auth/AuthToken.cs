namespace ServerManagement.Infrastructure.Auth;

public record AuthToken(string Jwt, DateTime Expiry);
