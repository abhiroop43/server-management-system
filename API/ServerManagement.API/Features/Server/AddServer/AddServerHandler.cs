namespace ServerManagement.API.Features.Server.AddServer;

public class AddServerHandler(ApplicationDbContext dbContext)
    : ICommandHandler<AddServerCommand, AddServerResult>
{
    public async Task<AddServerResult> Handle(
        AddServerCommand command,
        CancellationToken cancellationToken
    )
    {
        var server = command.Adapt<Domain.Entities.Server>();
        Domain.Entities.Server.Create(server);
        dbContext.Servers.Add(server);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new AddServerResult(true);
    }
}
