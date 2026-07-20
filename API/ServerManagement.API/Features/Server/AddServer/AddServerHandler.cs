namespace ServerManagement.API.Features.Server.AddServer;

public class AddServerHandler(
    ApplicationDbContext dbContext,
    IHttpContextAccessor httpContextAccessor
) : ICommandHandler<AddServerCommand, AddServerResult>
{
    public async Task<AddServerResult> Handle(
        AddServerCommand request,
        CancellationToken cancellationToken
    )
    {
        return new AddServerResult(true);
    }
}
