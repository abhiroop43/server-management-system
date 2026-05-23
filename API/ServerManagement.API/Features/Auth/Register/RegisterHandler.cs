using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using ServerManagement.Domain.CQRS;
using ServerManagement.Infrastructure.Auth;

namespace ServerManagement.API.Features.Auth.Register;

public class RegisterCommandHandler(
    UserManager<ApplicationUser> userManager,
    IEmailSender<ApplicationUser> emailSender,
    IConfiguration configuration
) : ICommandHandler<RegisterUserCommand, RegisterUserResult>
{
    public async Task<RegisterUserResult> Handle(
        RegisterUserCommand command,
        CancellationToken cancellationToken
    )
    {
        var user = new ApplicationUser
        {
            UserName = command.Email,
            Email = command.Email,
            FirstName = command.FirstName!,
            LastName = command.LastName,
            DateOfBirth = command.DateOfBirth,
        };
        var result = await userManager.CreateAsync(user, command.Password!);

        if (!result.Succeeded)
        {
            var validationErrors = result.Errors.Select(err => $"{err.Code} : {err.Description}");
            throw new ValidationException(string.Join("; ", validationErrors));
        }
        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

        var confirmUrl =
            $"{configuration["Frontend:BaseUrl"]}/confirm-email?userId={user.Id}&token={Uri.EscapeDataString(token)}";

        await emailSender.SendConfirmationLinkAsync(
            user,
            "Confirm your email",
            $"Click here to confirm: {confirmUrl}"
        );
        return new RegisterUserResult(true);
    }
}
