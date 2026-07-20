using FluentValidation;

namespace ServerManagement.API.Features.Server.AddServer;

public record AddServerCommand(
    string Name,
    bool IsOnline,
    string Status,
    string HostName,
    string PrimaryIp,
    List<string> IpAddresses,
    string MacAddress,
    string OperatingSystem,
    string GeographicRegion,
    int CpuCores,
    double MemoryInGb,
    TimeSpan Uptime,
    DateTimeOffset LastSeen,
    DateTimeOffset? DecommissionedAt,
    decimal HealthScore,
    List<string> Tags,
    Dictionary<string, string> Metadata,
    Guid? OwnerId,
    string Notes
) : ICommand<AddServerResult>;

public record AddServerResult(bool Success);
