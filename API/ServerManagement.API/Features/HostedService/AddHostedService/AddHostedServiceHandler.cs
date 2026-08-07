namespace ServerManagement.API.Features.HostedService.AddHostedService;

public class AddHostedServiceHandler(ApplicationDbContext dbContext)
    : ICommandHandler<AddHostedServiceCommand, AddHostedServiceResult>
{
    public async Task<AddHostedServiceResult> Handle(
        AddHostedServiceCommand command,
        CancellationToken cancellationToken
    )
    {
        var hostedServiceId = Guid.NewGuid();
        var hostedService = Domain.Entities.HostedService.Add(
            HostedServiceId.Of(hostedServiceId),
            ServerId.Of(command.ServerId!.Value),
            HostedServiceName.Of(command.HostedServiceName),
            command.Port,
            command.IsListening,
            DateTimeOffset.Now
        );

        await dbContext.HostedServices.AddAsync(hostedService, cancellationToken);
        var savedRecords = await dbContext.SaveChangesAsync(cancellationToken);

        return savedRecords > 0
            ? new AddHostedServiceResult(hostedServiceId, true)
            : new AddHostedServiceResult(Guid.Empty, false);
    }
}
