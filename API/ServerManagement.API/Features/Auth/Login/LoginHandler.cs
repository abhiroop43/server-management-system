namespace ServerManagement.API.Features.Auth.Login;

public class LoginHandler(
    UserManager<ApplicationUser> userManager,
    IEmailSender<ApplicationUser> emailSender,
    IConfiguration configuration
) : ICommandHandler<LoginCommand, LoginResult>
{
    public Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
