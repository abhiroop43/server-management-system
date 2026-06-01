namespace ServerManagement.Domain.Exceptions;

public class UserUnauthorizedException(string? message)
    : Exception($"User is not authorized to access this resouce. {message}") { }
