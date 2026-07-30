namespace ServerManagement.API.Dtos;

public record DiskDto(
    Guid Id,
    string Name,
    long CapacityGb,
    long UsedGb,
    string DiskType,
    bool IsActive,
    string? CreatedBy,
    DateTime? CreatedDate,
    string? UpdatedBy,
    DateTime? UpdatedDate
);
