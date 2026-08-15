namespace ServerManagement.API.Features.HostedService.UpdateHostedService;

public record UpdateHostedServiceRequest(
    Guid? Id,
    Guid? ServerId,
    string HostedServiceName,
    int Port,
    bool IsListening
);

public class UpdateHostedServiceEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut(
            "/servers/{serverId:guid}/services",
            async (
                [FromRoute] Guid serverId,
                [FromBody] UpdateHostedServiceRequest request,
                ISender sender
            ) =>
            {
                var mappedCmd = request.Adapt<UpdateHostedServiceCommand>();
                var command = mappedCmd with { ServerId = serverId };

                var result = await sender.Send(command);

                return result.Success
                    ? Results.Ok(
                        new ApiResponseDto(
                            StatusCodes.Status200OK,
                            "Service updated successfully",
                            result
                        )
                    )
                    : Results.BadRequest(
                        new ApiResponseDto(
                            StatusCodes.Status400BadRequest,
                            "Failed to update service. Please try again later",
                            result
                        )
                    );
            }
        );
    }
}
