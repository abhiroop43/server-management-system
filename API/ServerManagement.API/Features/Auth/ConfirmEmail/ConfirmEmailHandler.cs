namespace ServerManagement.API.Features.Auth.ConfirmEmail;

public record ConfirmEmailCommand(string UserId, string Token) : ICommand<ConfirmEmailResult>;

public record ConfirmEmailResult(bool Success);

public class ConfirmEmailHandler(UserManager<ApplicationUser> userManager)
    : ICommandHandler<ConfirmEmailCommand, ConfirmEmailResult>
{
    public async Task<ConfirmEmailResult> Handle(
        ConfirmEmailCommand command,
        CancellationToken cancellationToken
    )
    {
        var user = await userManager.FindByIdAsync(command.UserId);

        if (user == null)
        {
            throw new BadRequestException($"No user exists with Id: {command.UserId}");
        }

        var emailAlreadyConfirmed = await userManager.IsEmailConfirmedAsync(user);

        if (emailAlreadyConfirmed)
        {
            throw new BadRequestException("User's email is already confirmed");
        }

        var confirmation = await userManager.ConfirmEmailAsync(
            user,
            Uri.UnescapeDataString(command.Token)
        );

        if (confirmation.Succeeded)
        {
            return new ConfirmEmailResult(true);
        }

        var errorDetails = string.Join(
            ";",
            confirmation.Errors.Select(e => $"{e.Code} : {e.Description}")
        );

        throw new BadRequestException(errorDetails);
    }
}
