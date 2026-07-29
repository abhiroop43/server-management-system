using FluentValidation;

namespace ServerManagement.API.Features.Server.UpdateServer;

public record UpdateServerCommand(
    Guid Id,
    string Name,
    string HostName,
    string PrimaryIpAddress,
    int CpuCores,
    double MemoryInGb,
    string Status,
    Guid? OwnerId,
    List<string>? Tags,
    Dictionary<string, string>? Metadata,
    List<string>? IpAddresses,
    string GeographicRegion
) : ICommand<UpdateServerResult>;

public record UpdateServerResult(bool Success);

public class UpdateServerCommandValidator : AbstractValidator<UpdateServerCommand>
{
    private const string RequiredFieldErrorMessage = "{PropertyName} cannot be empty";
    private const string GreaterThanZeroErrorMessage = "{PropertyName} must be greater than 0";
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly ApplicationIdentityDbContext _identityDbContext;

    public UpdateServerCommandValidator(
        ApplicationIdentityDbContext identityDbContext,
        ApplicationDbContext applicationDbContext
    )
    {
        _identityDbContext = identityDbContext;
        _applicationDbContext = applicationDbContext;
        RuleFor(x => x.Id).NotEmpty().WithMessage(RequiredFieldErrorMessage);
        RuleFor(x => x.Name).NotEmpty().WithMessage(RequiredFieldErrorMessage);
        RuleFor(x => x.GeographicRegion).NotEmpty().WithMessage(RequiredFieldErrorMessage);
        RuleFor(x => x.PrimaryIpAddress).NotEmpty().WithMessage(RequiredFieldErrorMessage);
        RuleFor(x => x.HostName).NotEmpty().WithMessage(RequiredFieldErrorMessage);
        RuleFor(x => x.CpuCores).GreaterThan(0).WithMessage(GreaterThanZeroErrorMessage);
        RuleFor(x => x.MemoryInGb).GreaterThan(0).WithMessage(GreaterThanZeroErrorMessage);
        RuleFor(x => x.OwnerId)
            .MustAsync(ExistingUserAsync)
            .WithMessage("{PropertyName} must belong to an active user");
        RuleFor(x => x.Id)
            .MustAsync(ExistingServerAsync)
            .WithMessage("{PropertyName} must belong to an existing server");
    }

    private async Task<bool> ExistingServerAsync(Guid serverId, CancellationToken cancellationToken)
    {
        if (serverId == Guid.Empty)
            return false;

        var server = await _applicationDbContext
            .Servers.AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == ServerId.Of(serverId), cancellationToken);

        return server != null;
    }

    private async Task<bool> ExistingUserAsync(Guid? ownerId, CancellationToken cancellationToken)
    {
        if (ownerId == null || ownerId == Guid.Empty)
            return true; // as ownerId is not mandatory

        var user = await _identityDbContext
            .ApplicationUsers.AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.Id == ownerId.ToString() && x.EmailConfirmed == true,
                cancellationToken
            );

        return user != null;
    }
}
