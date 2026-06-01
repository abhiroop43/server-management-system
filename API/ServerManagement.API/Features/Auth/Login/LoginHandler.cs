using ServerManagement.Domain.Exceptions;
using ServerManagement.Infrastructure.Services;

namespace ServerManagement.API.Features.Auth.Login;

public class LoginHandler(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    JwtTokenService jwtTokenService,
    IConfiguration configuration
) : ICommandHandler<LoginCommand, LoginResult>
{
    public async Task<LoginResult> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(command.Email);

        if (user is null)
        {
            return new LoginResult(false, null, null);
        }

        var isUserConfirmed = await userManager.IsEmailConfirmedAsync(user);

        if (!isUserConfirmed)
        {
            throw new UserUnauthorizedException("User has not confirmed the email.");
        }

        var maxLoginAttempts = Convert.ToInt32(configuration["MaxLoginAttempts"]!);
        var accountLockoutOnNextAttempt = user.AccessFailedCount < maxLoginAttempts;

        var result = await signInManager.CheckPasswordSignInAsync(
            user,
            command.Password,
            accountLockoutOnNextAttempt
        );

        if (!result.Succeeded)
        {
            throw new UserUnauthorizedException(
                "Login failed. Either the username or the password is incorrect."
            );
        }

        var authToken = await jwtTokenService.GenerateJwtTokenAsync(user);

        return new LoginResult(true, authToken.Jwt, authToken.Expiry);
    }
}
