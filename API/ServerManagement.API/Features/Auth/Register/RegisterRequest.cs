using FluentValidation;
using ServerManagement.Domain.CQRS;

namespace ServerManagement.API.Features.Auth.Register;

public record RegisterUserCommand(
    string? Email,
    string? FirstName,
    string LastName,
    string? Password,
    DateTime DateOfBirth
) : ICommand<RegisterUserResult>;

public record RegisterUserResult(bool Success);

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        const string requiredErrorMessage = "{PropertyName} is required.";
        RuleFor(c => c.Email).NotEmpty().WithMessage(requiredErrorMessage);
        RuleFor(c => c.Password)
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(15)
            .WithMessage(requiredErrorMessage);
        RuleFor(c => c.FirstName).NotEmpty().WithMessage(requiredErrorMessage);
    }
}
