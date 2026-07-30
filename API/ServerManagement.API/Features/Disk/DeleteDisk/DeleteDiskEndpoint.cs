namespace ServerManagement.API.Features.Disk.DeleteDisk;

public class DeleteDiskEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete(
                "/servers/{serverId:guid}/disks/{diskId:guid}",
                async ([FromRoute] Guid serverId, [FromRoute] Guid diskId, ISender sender) =>
                {
                    var command = new DeleteDiskCommand(diskId);
                    var result = await sender.Send(command);

                    return result.Success
                        ? Results.NoContent()
                        : Results.BadRequest(
                            new ApiResponseDto(
                                StatusCodes.Status400BadRequest,
                                "Unable to delete this disk. It may already have been deleted",
                                null
                            )
                        );
                }
            )
            .RequireAuthorization()
            .WithName("DeleteDisk")
            .Produces<ApiResponseDto>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete a disk using disk Id")
            .WithDescription("Delete a disk for a server using the disk Id");
    }
}
