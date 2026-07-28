using FluentValidation;

namespace ServerManagement.API.Features.Server.AddServer;

public record AddServerCommand(
    string Name,
    Domain.Enums.OperationStatus Status,
    string HostName,
    string PrimaryIp,
    List<string> IpAddresses,
    string MacAddress,
    Domain.Enums.OperatingSystem OperatingSystem,
    string GeographicRegion,
    int CpuCores,
    double MemoryInGb,
    List<string> Tags,
    Dictionary<string, string> Metadata,
    Guid? OwnerId,
    string Notes
) : ICommand<AddServerResult>;

public record AddServerResult(Guid Id, bool Success);

public class AddServerCommandValidator : AbstractValidator<AddServerCommand>
{
    private const string RequiredFieldErrorMessage = "{PropertyName} cannot be empty";

    private const string GreaterThanErrorMessage =
        "{PropertyName} must be greater than {MinLength}";

    public AddServerCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage(RequiredFieldErrorMessage);
        RuleFor(x => x.GeographicRegion).NotEmpty().WithMessage(RequiredFieldErrorMessage);
        RuleFor(x => x.PrimaryIp).NotEmpty().WithMessage(RequiredFieldErrorMessage);
        RuleFor(x => x.MacAddress).NotEmpty().WithMessage(RequiredFieldErrorMessage);
        RuleFor(x => x.HostName).NotEmpty().WithMessage(RequiredFieldErrorMessage);
        RuleFor(x => x.CpuCores).GreaterThan(0).WithMessage(GreaterThanErrorMessage);
        RuleFor(x => x.MemoryInGb).GreaterThan(0).WithMessage(GreaterThanErrorMessage);
        // validate if OwnerId belongs to an active user
    }
}
