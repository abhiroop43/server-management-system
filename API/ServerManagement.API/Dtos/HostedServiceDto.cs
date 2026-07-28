namespace ServerManagement.API.Dtos;

public record HostedServiceDto(
    Guid Id,
    string ServiceName,
    int Port,
    bool IsListening,
    DateTimeOffset LastChecked,
    string? CreatedBy,
    DateTime? CreatedDate,
    string? UpdatedBy,
    DateTime? UpdatedDate
);
