namespace ServerManagement.API.Dtos;

public record ServerDto(
    Guid Id,
    string Name,
    bool IsOnline,
    string Status,
    string HostName,
    string PrimaryIpAddress
);
