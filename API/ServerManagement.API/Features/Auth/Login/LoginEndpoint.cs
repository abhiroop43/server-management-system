namespace ServerManagement.API.Features.Auth.Login;

public record LoginUserRequest(string? Email, string? Password);

public record LoginUserResponse(bool Success, string? Token, DateTime? Expiry);

public class LoginEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(
                "/auth/login",
                async ([FromBody] LoginUserRequest request, ISender sender) =>
                {
                    var command = request.Adapt<LoginCommand>();
                    var result = await sender.Send(command);

                    var response = result.Adapt<LoginUserResponse>();

                    return response.Success ? Results.Ok(response) : Results.Unauthorized();
                }
            )
            .WithName("Login")
            .Produces<LoginUserResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .WithSummary("Login a user")
            .WithDescription("Login an already registered user");
    }
}
