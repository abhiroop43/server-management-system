namespace ServerManagement.API.Features.Auth.ValidateToken;

public record ValidateTokenResponse(bool Valid, ApplicationUserDto User);

public class ValidateTokenEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(
                "/auth/validate",
                async (ISender sender) =>
                {
                    var command = new ValidateTokenCommand();
                    var result = await sender.Send(command);

                    var response = result.Adapt<ValidateTokenResponse>();

                    return response.Valid ? Results.Ok(response) : Results.BadRequest();
                }
            )
            .RequireAuthorization()
            .WithName("ValidateToken")
            .Produces<ValidateTokenResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .WithSummary("Validate Token")
            .WithDescription("Validate a user's token and provide user details in response");
    }
}
