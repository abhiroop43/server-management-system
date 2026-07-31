namespace ServerManagement.API.Features.HostedService.AddHostedService;

public record AddHostedServiceCommand(
    Guid? ServerId,
    string HostedServiceName,
    int Port,
    bool IsListening
) : ICommand<AddHostedServiceResult>;

public record AddHostedServiceResult(Guid Id, bool Success);

public class AddHostedServiceCommandValidator : AbstractValidator<AddHostedServiceCommand>
{
    private const string RequiredFieldErrorMessage = "{PropertName} cannot be empty";

    private const string GreaterThanErrorMessage =
        "{PropertyName} must be greater than {MinLength}";

    private readonly ApplicationDbContext _dbContext;

    public AddHostedServiceCommandValidator(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(x => x.ServerId)
            .NotEmpty()
            .WithMessage(RequiredFieldErrorMessage)
            .MustAsync(ServerIdExistsAsync)
            .WithMessage("{PropertyName} must be a valid Server Id");
        RuleFor(x => x.HostedServiceName).NotEmpty().WithMessage(RequiredFieldErrorMessage);
        RuleFor(x => x.Port).GreaterThan(0).WithMessage(GreaterThanErrorMessage);
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
