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

        Domain.Entities.Disk.Add(
            DiskId.Of(diskId),
            ServerId.Of(command.ServerId!.Value),
            DiskName.Of(command.Name),
            command.CapacityGb,
            command.UsedGb,
            Enum.Parse<DiskType>(command.DiskType)
        );

        var savedRows = await dbContext.SaveChangesAsync(cancellationToken);

        return new AddDiskResult(diskId, savedRows > 0);
    }
}
