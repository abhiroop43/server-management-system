namespace ServerManagement.API.Features.Auth.ConfirmEmail;

public record ConfirmEmailRequest(string? UserId, string? Token);

public record ConfirmEmailResponse(bool Success);

public class ConfirmEmailEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "/auth/confirm-email",
                async ([FromQuery] string userId, [FromQuery] string token, ISender sender) =>
                {
                    var command = new ConfirmEmailCommand(userId, token);

                    var result = await sender.Send(command);

                    var response = result.Adapt<ConfirmEmailResponse>();

                    return response.Success ? Results.Ok(response) : Results.BadRequest(response);
                }
            )
            .WithName("Confirm Email")
            .Produces<ConfirmEmailResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Confirm User's Email")
            .WithDescription("Confirm the email of a recently registered user so they can login");
    }
}
