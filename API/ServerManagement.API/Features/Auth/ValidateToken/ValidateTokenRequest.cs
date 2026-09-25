namespace ServerManagement.API.Features.Auth.ValidateToken;

public record ValidateTokenCommand() : ICommand<ValidateTokenResult>;

public record ValidateTokenResult(bool Valid, ApplicationUserDto User);
