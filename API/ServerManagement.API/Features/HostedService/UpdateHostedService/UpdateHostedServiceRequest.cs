namespace ServerManagement.API.Features.HostedService.UpdateHostedService;

public record UpdateHostedServiceCommand(
    Guid? Id,
    Guid? ServerId,
    string HostedServiceName,
    int Port,
    bool IsListening
) : ICommand<UpdateHostedServiceResult>;

public record UpdateHostedServiceResult(bool Success);

public class UpdateHostedServiceCommandValidator : AbstractValidator<UpdateHostedServiceCommand>
{
    private const string RequiredFieldErrorMessage = "{PropertyName} cannot be empty";

    private const string GreaterThanErrorMessage =
        "{PropertyName} must be greater than {MinLength}";

    private readonly ApplicationDbContext _dbContext;

    public UpdateHostedServiceCommandValidator(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(RequiredFieldErrorMessage)
            .MustAsync(IdExistsAsync)
            .WithMessage("{PropertyName} must be an existing Service Id");
        RuleFor(x => x.ServerId)
            .NotEmpty()
            .WithMessage(RequiredFieldErrorMessage)
            .MustAsync(ServerIdExistsAsync)
            .WithMessage("{PropertyName} must be a valid Server Id");
        RuleFor(x => x.HostedServiceName).NotEmpty().WithMessage(RequiredFieldErrorMessage);
        RuleFor(x => x.Port).GreaterThan(0).WithMessage(GreaterThanErrorMessage);
    }

    private async Task<bool> IdExistsAsync(Guid? serviceId, CancellationToken cancellationToken)
    {
        if (serviceId == null || serviceId == Guid.Empty)
            return false;

        var existingService = await _dbContext
            .HostedServices.AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.Id == HostedServiceId.Of(serviceId.Value),
                cancellationToken
            );

        return existingService != null;
    }

    private async Task<bool> ServerIdExistsAsync(
        Guid? serverId,
        CancellationToken cancellationToken
    )
    {
        if (serverId == null || serverId == Guid.Empty)
            return false;

        var existingServer = await _dbContext
            .Servers.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == ServerId.Of(serverId.Value), cancellationToken);

        return existingServer != null;
    }
}
