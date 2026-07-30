using ServerManagement.Domain.Enums;

namespace ServerManagement.API.Features.Disk.AddDisk;

public class AddDiskHandler(ApplicationDbContext dbContext)
    : ICommandHandler<AddDiskCommand, AddDiskResult>
{
    public async Task<AddDiskResult> Handle(
        AddDiskCommand command,
        CancellationToken cancellationToken
    )
    {
        var diskId = Guid.NewGuid();

        var savedDisk = Domain.Entities.Disk.Add(
            DiskId.Of(diskId),
            ServerId.Of(command.ServerId!.Value),
            DiskName.Of(command.Name),
            command.CapacityGb,
            command.UsedGb,
            Enum.Parse<DiskType>(command.DiskType)
        );

        await dbContext.Disks.AddAsync(savedDisk, cancellationToken);

        var savedRecords = await dbContext.SaveChangesAsync(cancellationToken);

        return savedRecords > 0
            ? new AddDiskResult(diskId, true)
            : new AddDiskResult(Guid.Empty, false);
    }
}
