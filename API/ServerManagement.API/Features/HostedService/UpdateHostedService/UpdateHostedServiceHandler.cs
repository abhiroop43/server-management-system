namespace ServerManagement.API.Features.HostedService.UpdateHostedService;

public class UpdateHostedServiceCommandHandler(ApplicationDbContext dbContext)
    : ICommandHandler<UpdateHostedServiceCommand, UpdateHostedServiceResult>
{
    public async Task<UpdateHostedServiceResult> Handle(
        UpdateHostedServiceCommand command,
        CancellationToken cancellationToken
    )
    {
        var hostedService = await dbContext.HostedServices.FirstOrDefaultAsync(
            x =>
                x.Id == HostedServiceId.Of(command.Id!.Value)
                && x.ServerId == ServerId.Of(command.ServerId!.Value),
            cancellationToken
        );

        if (hostedService == null)
        {
            throw new NotFoundException("Service is not found or does not belong to this server");
        }

        hostedService.Update(
            HostedServiceName.Of(command.HostedServiceName),
            command.Port,
            command.IsListening,
            DateTimeOffset.UtcNow
        );

        var savedRecords = await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateHostedServiceResult(savedRecords > 0);
    }
}
