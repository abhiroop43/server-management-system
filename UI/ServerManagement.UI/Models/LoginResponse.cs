namespace ServerManagement.UI.Models;

public record LoginResponse(bool Success, string? Token, DateTime? Expiry);
