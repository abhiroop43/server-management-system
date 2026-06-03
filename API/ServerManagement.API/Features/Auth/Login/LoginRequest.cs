using FluentValidation;

namespace ServerManagement.API.Features.Auth.Login;

public record LoginCommand(string Email, string Password) : ICommand<LoginResult>;

public record LoginResult(bool Success, string? Token, DateTime? Expiry);

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        const string requiredErrorMessage = "{PropertyName} is required.";

        RuleFor(x => x.Email).NotEmpty().WithMessage(requiredErrorMessage);
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage(requiredErrorMessage)
            .MinimumLength(8)
            .WithMessage("{PropertyName} must be at least {MinLength} characters.")
            .MaximumLength(15)
            .WithMessage("{PropertyName} must be at most {MaxLength} characters.");
    }
}
