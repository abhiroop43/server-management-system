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

public class AddServerCommandValidator : AbstractValidator<AddServerCommand>
{
    public AddServerCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .MinimumLength(3)
            .WithMessage("{PropertyName} must be at least {MinLength} characters long")
            .MaximumLength(128)
            .WithMessage("{PropertyName} must be at most {MaxLength} characters long");

        RuleFor(x => x.HostName)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .MinimumLength(3)
            .WithMessage("{PropertyName} must be at least {MinLength} characters long")
            .MaximumLength(128)
            .WithMessage("{PropertyName} must be at most {MaxLength} characters long");
    }
}
