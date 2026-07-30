namespace ServerManagement.API.Features.Disk.UpdateDisk;

public record UpdateDiskRequest(
    Guid? Id,
    string Name,
    long CapacityGb,
    long UsedGb,
    string DiskType,
    bool IsActive
);

public record UpdateDiskResponse(bool Success);

public class UpdateDiskEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut(
                "/servers/{serverId:guid}/disks",
                async (
                    [FromRoute] Guid serverId,
                    [FromBody] UpdateDiskRequest request,
                    ISender sender
                ) =>
                {
                    var mappedCmd = request.Adapt<UpdateDiskCommand>();
                    var command = mappedCmd with { ServerId = serverId };

                    var result = await sender.Send(command);
                    var response = result.Adapt<UpdateDiskResponse>();

                    if (response.Success)
                    {
                        return new ApiResponseDto(
                            StatusCodes.Status200OK,
                            "Disk updated successfully",
                            response
                        );
                    }

                    return new ApiResponseDto(
                        StatusCodes.Status400BadRequest,
                        "Failed to update disk. Please try again later",
                        response
                    );
                }
            )
            .RequireAuthorization()
            .WithName("UpdateDisk")
            .Produces<ApiResponseDto>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update a disk in the server")
            .WithDescription("Update a disk using disk Id in the server");
    }
}
