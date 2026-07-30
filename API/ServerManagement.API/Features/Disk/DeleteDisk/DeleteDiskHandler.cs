namespace ServerManagement.API.Features.Disk.DeleteDisk;

public record DeleteDiskCommand(Guid Id) : ICommand<DeleteDiskResult>;

public record DeleteDiskResult(bool Success);

public class DeleteDiskCommandHandler(ApplicationDbContext dbContext)
    : ICommandHandler<DeleteDiskCommand, DeleteDiskResult>
{
    public async Task<DeleteDiskResult> Handle(
        DeleteDiskCommand command,
        CancellationToken cancellationToken
    )
    {
        var disk = await dbContext.Disks.FirstOrDefaultAsync(
            x => x.Id == DiskId.Of(command.Id) && x.IsActive,
            cancellationToken
        );

        if (disk == null)
        {
            throw new NotFoundException(nameof(disk), command.Id);
        }

        disk.Remove();
        var savedRecords = await dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteDiskResult(savedRecords > 0);
    }
}
