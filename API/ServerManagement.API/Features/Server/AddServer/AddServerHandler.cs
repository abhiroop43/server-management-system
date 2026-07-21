namespace ServerManagement.API.Features.Server.AddServer;

public class AddServerHandler(ApplicationDbContext dbContext)
    : ICommandHandler<AddServerCommand, AddServerResult>
{
    public async Task<AddServerResult> Handle(
        AddServerCommand command,
        CancellationToken cancellationToken
    )
    {
        var serverId = Guid.NewGuid();

        var server = Domain.Entities.Server.Create(
            ServerId.Of(serverId),
            command.OwnerId,
            ServerName.Of(command.Name),
            HostName.Of(command.HostName),
            PrimaryIpAddress.Of(command.PrimaryIp),
            command.CpuCores,
            command.MemoryInGb,
            command.Notes,
            command.Status,
            command.Tags,
            command.Metadata,
            command.IpAddresses,
            command.OperatingSystem,
            command.GeographicRegion
        );
        dbContext.Servers.Add(server);
        var savedRecords = await dbContext.SaveChangesAsync(cancellationToken);

        return new AddServerResult(savedRecords > 0);
    }
}
