namespace ServerManagement.Infrastructure.External;

public class EmailSender(IResend resend, IConfiguration configuration)
    : IEmailSender<ApplicationUser>
{
    public async Task SendConfirmationLinkAsync(
        ApplicationUser user,
        string email,
        string confirmationLink
    )
    {
        var emailBody = $"""
            <h1>Confirmation</h1>
            URL: {confirmationLink}
            """;
        if (user.Email != null)
            await SendEmailAsync(user.Email, email, emailBody);
    }

    public async Task SendPasswordResetLinkAsync(
        ApplicationUser user,
        string email,
        string resetLink
    )
    {
        var emailBody = $"""
            <h1>Reset Password</h1>
            URL: {resetLink}
            """;
        if (user.Email != null)
            await SendEmailAsync(user.Email, email, emailBody);
    }

    public async Task SendPasswordResetCodeAsync(
        ApplicationUser user,
        string email,
        string resetCode
    )
    {
        var emailBody = $"""
            <h1>Reset Password</h1>
            Code: {resetCode}
            """;
        if (user.Email != null)
            await SendEmailAsync(user.Email, email, emailBody);
    }

    public async Task SendEmailAsync(string emailAddress, string subject, string htmlMessage)
    {
        var message = new EmailMessage();
        message.From = configuration["NotificationsFromAddress"]!;
        message.To.Add(emailAddress);
        message.Subject = subject;
        message.HtmlBody = htmlMessage;

        await resend.EmailSendAsync(message);
    }
}
