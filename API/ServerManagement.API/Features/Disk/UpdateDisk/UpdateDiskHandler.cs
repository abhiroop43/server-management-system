namespace ServerManagement.API.Features.Disk.UpdateDisk;

public class UpdateDiskCommandHandler(ApplicationDbContext dbContext)
    : ICommandHandler<UpdateDiskCommand, UpdateDiskResult>
{
    public async Task<UpdateDiskResult> Handle(
        UpdateDiskCommand command,
        CancellationToken cancellationToken
    )
    {
        var disk = await dbContext.Disks.FirstOrDefaultAsync(
            x =>
                x.Id == DiskId.Of(command.Id!.Value)
                && x.ServerId == ServerId.Of(command.ServerId!.Value),
            cancellationToken
        );

        if (disk == null)
        {
            throw new NotFoundException("Disk is not found or does not belong to this server");
        }

        disk.Update(
            DiskName.Of(command.Name),
            command.CapacityGb,
            command.UsedGb,
            Enum.Parse<DiskType>(command.DiskType)
        );

        var savedRecords = await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateDiskResult(savedRecords > 0);
    }
}
