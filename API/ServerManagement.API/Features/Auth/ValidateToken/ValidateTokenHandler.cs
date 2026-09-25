namespace ServerManagement.API.Features.Auth.ValidateToken;

public class ValidateTokenHandler(IJwtTokenService jwtTokenService)
    : ICommandHandler<ValidateTokenCommand, ValidateTokenResult>
{
    public async Task<ValidateTokenResult> Handle(
        ValidateTokenCommand command,
        CancellationToken cancellationToken
    )
    {
        var user = await jwtTokenService.ValidateJwtTokenAsync();

        return new ValidateTokenResult(true, user.Adapt<ApplicationUserDto>());
    }
}
