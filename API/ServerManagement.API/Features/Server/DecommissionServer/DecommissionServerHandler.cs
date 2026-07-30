namespace ServerManagement.API.Features.Server.DecommissionServer;

public record DecommissionServerCommand(Guid ServerId) : ICommand<DecommissionServerResult>;

public record DecommissionServerResult(bool Success);

public class DecommissionServerCommandHandler(ApplicationDbContext dbContext)
    : ICommandHandler<DecommissionServerCommand, DecommissionServerResult>
{
    public async Task<DecommissionServerResult> Handle(
        DecommissionServerCommand command,
        CancellationToken cancellationToken
    )
    {
        var server = await dbContext.Servers.FirstOrDefaultAsync(
            s => s.Id == ServerId.Of(command.ServerId) && s.IsOnline,
            cancellationToken
        );

        if (server == null)
        {
            throw new NotFoundException(nameof(server), command.ServerId);
        }

        server.DecommissionServer();

        var result = await dbContext.SaveChangesAsync(cancellationToken);

        return new DecommissionServerResult(result > 0);
    }
}
