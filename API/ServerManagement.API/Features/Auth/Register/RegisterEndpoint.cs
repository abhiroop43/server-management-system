namespace ServerManagement.API.Features.Auth.Register;

public record RegisterUserRequest(
    string? Email,
    string? FirstName,
    string? LastName,
    string? Password,
    DateTime DateOfBirth
);

public record RegisterUserResponse(bool Succeeded);

public class RegisterEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(
                "/auth/register",
                async ([FromBody] RegisterUserRequest request, ISender sender) =>
                {
                    var command = request.Adapt<RegisterUserCommand>();
                    var result = await sender.Send(command);

                    var response = result.Adapt<RegisterUserResponse>();

                    return result.Success
                        ? Results.Created("/auth/register", response)
                        : Results.BadRequest();
                }
            )
            .WithName("Register")
            .Produces<RegisterUserResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Register a user")
            .WithDescription("Registers a user and send confirmation email");
    }
}
