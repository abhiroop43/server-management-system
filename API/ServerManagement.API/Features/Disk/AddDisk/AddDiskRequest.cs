using FluentValidation;

namespace ServerManagement.API.Features.Disk.AddDisk;

public record AddDiskCommand(
    Guid? ServerId,
    string Name,
    long CapacityGb,
    long UsedGb,
    string DiskType,
    bool IsActive
) : ICommand<AddDiskResult>;

public record AddDiskResult(Guid Id, bool Success);

public class AddDiskCommandValidator : AbstractValidator<AddDiskCommand>
{
    private const string RequiredFieldErrorMessage = "{PropertyName} cannot be empty";

    private const string GreaterThanErrorMessage =
        "{PropertyName} must be greater than {MinLength}";

    public AddDiskCommandValidator()
    {
        RuleFor(d => d.Name).NotEmpty().WithMessage(RequiredFieldErrorMessage);
        RuleFor(d => d.ServerId).NotEmpty().WithMessage(RequiredFieldErrorMessage);
        RuleFor(d => d.CapacityGb).GreaterThan(0).WithMessage(GreaterThanErrorMessage);
        //validate serverId
    }
}
