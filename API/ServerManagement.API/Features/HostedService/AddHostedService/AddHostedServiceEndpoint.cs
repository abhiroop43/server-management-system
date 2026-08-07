namespace ServerManagement.API.Features.HostedService.AddHostedService;

public record AddHostedServiceRequest(string HostedServiceName, int Port, bool IsListening);

public class AddHostedServiceEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(
                "/servers/{serverId:guid}/services",
                async (
                    [FromRoute] Guid serverId,
                    [FromBody] AddHostedServiceRequest request,
                    ISender sender
                ) =>
                {
                    var mappedCommand = request.Adapt<AddHostedServiceCommand>();
                    var command = mappedCommand with { ServerId = serverId };

                    var result = await sender.Send(command);

                    if (!result.Success)
                    {
                        return Results.BadRequest(
                            new ApiResponseDto(
                                StatusCodes.Status400BadRequest,
                                "Failed to create service. Please try again later",
                                result
                            )
                        );
                    }

                    var response = new ApiResponseDto(
                        StatusCodes.Status201Created,
                        "Service created successfully",
                        result
                    );

                    return Results.Created($"/servers/{serverId}/services/{result.Id}", response);
                }
            )
            .RequireAuthorization()
            .WithName("AddHostedService")
            .Produces<ApiResponseDto>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Add a new hosted service")
            .WithDescription(
                "Add a new hosted service for an existing server, a valid Server Id is required."
            );
    }
}
