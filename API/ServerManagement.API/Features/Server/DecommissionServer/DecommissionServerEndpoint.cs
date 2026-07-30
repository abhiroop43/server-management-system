namespace ServerManagement.API.Features.Server.DecommissionServer;

public record DecommissionServerResponse(bool Success);

public class DecommissionServerEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete(
                "/servers/{serverId:guid}",
                async ([FromRoute] Guid serverId, ISender sender) =>
                {
                    var command = new DecommissionServerCommand(serverId);
                    var result = await sender.Send(command);

                    var response = result.Adapt<DecommissionServerResponse>();

                    if (response.Success)
                    {
                        return new ApiResponseDto(
                            StatusCodes.Status204NoContent,
                            "Server Decommissioned successfully",
                            response
                        );
                    }

                    return new ApiResponseDto(
                        StatusCodes.Status400BadRequest,
                        "Failed to decommission the server. Please try again later",
                        response
                    );
                }
            )
            .RequireAuthorization()
            .WithName("DecommissionServer")
            .Produces<ApiResponseDto>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Decommission a server")
            .WithDescription("Decommission a server using its Id");
    }
}
