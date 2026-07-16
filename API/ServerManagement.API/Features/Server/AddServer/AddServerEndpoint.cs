namespace ServerManagement.API.Features.Server.AddServer;

public record AddServerRequest();

public record AddServerResponse(bool Success);

public class AddServerEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(
                "/server",
                async ([FromBody] AddServerRequest addServerRequest, ISender sender) => { }
            )
            .WithName("AddServer")
            .Produces<AddServerResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Add Server")
            .WithDescription("Create a new server");
    }
}
