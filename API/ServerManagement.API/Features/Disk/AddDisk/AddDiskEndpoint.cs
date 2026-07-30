namespace ServerManagement.API.Features.Disk.AddDisk;

public record AddDiskRequest(
    string Name,
    long CapacityGb,
    long UsedGb,
    string DiskType,
    bool IsActive
);

public record AddDiskResponse(Guid Id, bool Success);

public class AddDiskEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(
            "/servers/{serverId:guid}/disks",
            async ([FromRoute] Guid serverId, [FromBody] AddDiskRequest request, ISender sender) =>
            {
                var mappedCmd = request.Adapt<AddDiskCommand>();
                var command = mappedCmd with { ServerId = serverId };

                var result = await sender.Send(command);

                var response = result.Adapt<AddDiskResponse>();

                if (!response.Success)
                {
                    return Results.BadRequest(
                        new ApiResponseDto(
                            StatusCodes.Status400BadRequest,
                            "Failed to add disk. Please try again later",
                            response
                        )
                    );
                }

                var apiResponse = new ApiResponseDto(
                    StatusCodes.Status201Created,
                    "Disk added successfully",
                    response
                );
                return Results.Created($"/servers/{serverId}/disks/{response.Id}", apiResponse);
            }
        );
    }
}
