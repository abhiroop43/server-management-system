namespace ServerManagement.API.Features.Disk.UpdateDisk;

public record UpdateDiskCommand(
    Guid? Id,
    Guid? ServerId,
    string Name,
    long CapacityGb,
    long UsedGb,
    string DiskType,
    bool IsActive
) : ICommand<UpdateDiskResult>;

public record UpdateDiskResult(bool Success);

public class UpdateDiskCommandValidator : AbstractValidator<UpdateDiskCommand>
{
    private const string RequiredFieldErrorMessage = "{PropertyName} cannot be empty";

    private const string GreaterThanErrorMessage =
        "{PropertyName} must be greater than {MinLength}";

    private readonly ApplicationDbContext _dbContext;

    public UpdateDiskCommandValidator(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(d => d.Name).NotEmpty().WithMessage(RequiredFieldErrorMessage);
        RuleFor(d => d.ServerId).NotEmpty().WithMessage(RequiredFieldErrorMessage);
        RuleFor(d => d.CapacityGb).GreaterThan(0).WithMessage(GreaterThanErrorMessage);
        RuleFor(d => d.ServerId)
            .NotEmpty()
            .WithMessage(RequiredFieldErrorMessage)
            .MustAsync(ServerExistsAsync)
            .WithMessage("ServerId does not exist or server might have been decommissioned");
        RuleFor(d => d.Id)
            .NotEmpty()
            .WithMessage(RequiredFieldErrorMessage)
            .MustAsync(DiskExistsAsync)
            .WithMessage("No disk exists with this Id");
    }

    private async Task<bool> DiskExistsAsync(Guid? diskId, CancellationToken cancellationToken)
    {
        if (diskId == null || diskId == Guid.Empty)
            return false;

        var disk = await _dbContext
            .Disks.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == DiskId.Of(diskId.Value), cancellationToken);

        return disk != null;
    }

    private async Task<bool> ServerExistsAsync(Guid? serverId, CancellationToken cancellationToken)
    {
        if (serverId == null || serverId == Guid.Empty)
            return false;

        var server = await _dbContext
            .Servers.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == ServerId.Of(serverId.Value), cancellationToken);

        return server != null && server.Status != OperationStatus.Decommissioned;
    }
}
