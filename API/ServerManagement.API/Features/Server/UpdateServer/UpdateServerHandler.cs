using ServerManagement.Domain.Enums;

namespace ServerManagement.API.Features.Server.UpdateServer;

public class UpdateServerCommandHandler(ApplicationDbContext dbContext)
    : ICommandHandler<UpdateServerCommand, UpdateServerResult>
{
    public async Task<UpdateServerResult> Handle(
        UpdateServerCommand command,
        CancellationToken cancellationToken
    )
    {
        var currentServer = await dbContext.Servers.FirstOrDefaultAsync(
            s => s.Id == ServerId.Of(command.Id) && s.Status != OperationStatus.Decommissioned,
            cancellationToken
        );

        if (currentServer == null)
        {
            throw new NotFoundException(nameof(currentServer), command.Id);
        }

        currentServer.Update(
            command.Name,
            command.HostName,
            command.PrimaryIpAddress,
            command.CpuCores,
            command.MemoryInGb,
            command.Status,
            command.OwnerId,
            command.Tags,
            command.Metadata,
            command.IpAddresses,
            command.GeographicRegion
        );

        var rowsUpdated = await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateServerResult(rowsUpdated > 0);
    }
}
