using FluentValidation;

namespace ServerManagement.API.Features.Server.UpdateServer;

public record UpdateServerCommand(
    Guid Id,
    string Name,
    string HostName,
    string PrimaryIpAddress,
    int CpuCores,
    double MemoryInGb,
    string Status,
    Guid? OwnerId,
    List<string>? Tags,
    Dictionary<string, string>? Metadata,
    List<string>? IpAddresses,
    string GeographicRegion
) : ICommand<UpdateServerResult>;

public record UpdateServerResult(bool Success);

public class UpdateServerCommandValidator : AbstractValidator<UpdateServerCommand>
{
    private const string RequiredFieldErrorMessage = "{PropertyName} cannot be empty";
    private const string GreaterThanZeroErrorMessage = "{PropertyName} must be greater than 0";

    public UpdateServerCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage(RequiredFieldErrorMessage);
        RuleFor(x => x.Name).NotEmpty().WithMessage(RequiredFieldErrorMessage);
        RuleFor(x => x.GeographicRegion).NotEmpty().WithMessage(RequiredFieldErrorMessage);
        RuleFor(x => x.PrimaryIpAddress).NotEmpty().WithMessage(RequiredFieldErrorMessage);
        RuleFor(x => x.HostName).NotEmpty().WithMessage(RequiredFieldErrorMessage);
        RuleFor(x => x.CpuCores).GreaterThan(0).WithMessage(GreaterThanZeroErrorMessage);
        RuleFor(x => x.MemoryInGb).GreaterThan(0).WithMessage(GreaterThanZeroErrorMessage);
        // validate if OwnerId belongs to an active user
    }
}
