using ServerManagement.API.Dtos;
using ServerManagement.Domain.Pagination;

namespace ServerManagement.API.Features.Disk.GetDisks;

public record GetDisksResponse(PaginationResult<DiskDto> Disks);

public class GetDisksEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "/disks",
                async (
                    [FromQuery] Guid serverId,
                    [FromQuery] int pageNumber,
                    [FromQuery] int pageSize,
                    ISender sender
                ) =>
                {
                    var query = new GetDisksQuery(
                        ServerId.Of(serverId),
                        new PaginationRequest((pageNumber - 1), pageSize)
                    );

                    var result = await sender.Send(query);

                    var response = result.Adapt<GetDisksResponse>();
                    var apiResponse = new ApiResponseDto(0, "Disks fetched successfully", response);

                    return response.Disks.Count > 0
                        ? Results.Ok(apiResponse)
                        : throw new NotFoundException("No disks found for this server");
                }
            )
            .WithName("GetDisks")
            .Produces<GetDisksResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get disks in a server")
            .WithDescription("Get paginated list of disks installed in a server");
    }
}
