using ServerManagement.API.Dtos;
using ServerManagement.Domain.Pagination;

namespace ServerManagement.API.Features.Disk.GetDisks;

public record GetDisksRequest(int pageIndex, int pageSize);

public record GetDisksResponse(PaginationResult<DiskDto> Disks);

public class GetDisksEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "/disks",
                async ([FromQuery] int pageNumber, [FromQuery] int pageSize, ISender sender) => { }
            )
            .WithName("GetDisks")
            .Produces<GetDisksResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get disks in a server")
            .WithDescription("Get paginated list of disks installed in a server");
    }
}
