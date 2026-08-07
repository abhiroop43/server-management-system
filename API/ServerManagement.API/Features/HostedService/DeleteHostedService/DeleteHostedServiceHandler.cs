namespace ServerManagement.API.Features.HostedService.DeleteHostedService;

public record DeleteHostedServiceCommand(Guid HostedServiceId, Guid ServerId)
    : ICommand<DeleteHostedServiceResult>;

public record DeleteHostedServiceResult(bool Success);

public class DeleteHostedServiceHandler(ApplicationDbContext dbContext)
    : ICommandHandler<DeleteHostedServiceCommand, DeleteHostedServiceResult>
{
    public async Task<DeleteHostedServiceResult> Handle(
        DeleteHostedServiceCommand command,
        CancellationToken cancellationToken
    )
    {
        var hostedService = await dbContext.HostedServices.FirstOrDefaultAsync(
            x =>
                x.Id == HostedServiceId.Of(command.HostedServiceId)
                && x.ServerId == ServerId.Of(command.ServerId)
                && x.IsActive,
            cancellationToken
        );

        if (hostedService == null)
        {
            throw new NotFoundException(nameof(hostedService), command.HostedServiceId);
        }

        hostedService.Remove();
        var savedRecords = await dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteHostedServiceResult(savedRecords > 0);
    }
}
