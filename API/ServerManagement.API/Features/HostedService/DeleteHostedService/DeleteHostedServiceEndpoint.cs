namespace ServerManagement.API.Features.HostedService.DeleteHostedService;

public class DeleteHostedServiceEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete(
                "/servers/{serverId:guid}/services/{serviceId:guid}",
                async ([FromRoute] Guid serverId, [FromRoute] Guid serviceId, ISender sender) =>
                {
                    var command = new DeleteHostedServiceCommand(serviceId, serverId);
                    var result = await sender.Send(command);

                    return result.Success
                        ? Results.NoContent()
                        : Results.BadRequest(
                            new ApiResponseDto(
                                StatusCodes.Status400BadRequest,
                                "Failed to delete hosted service. Please try again later",
                                result
                            )
                        );
                }
            )
            .RequireAuthorization()
            .WithName("DeleteHostedService")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete a hosted service")
            .WithDescription("Delete a hosted service using the Service Id and the Server Id");
    }
}
