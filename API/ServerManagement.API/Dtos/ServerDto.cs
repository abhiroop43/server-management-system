namespace ServerManagement.API.Dtos;

public record ServerDto(
    Guid Id,
    string Name,
    bool IsOnline,
    string Status,
    string HostName,
    string PrimaryIpAddress,
    string? CreatedBy,
    DateTime? CreatedDate,
    string? UpdatedBy,
    DateTime? UpdatedDate
);
