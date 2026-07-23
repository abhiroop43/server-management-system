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
                    [FromQuery] int? pageNumber,
                    [FromQuery] int? pageSize,
                    ISender sender
                ) =>
                {
                    if (pageNumber is null)
                    {
                        pageNumber = 1;
                    }

                    if (pageSize is null)
                    {
                        pageSize = 10;
                    }

                    var query = new GetDisksQuery(
                        ServerId.Of(serverId),
                        new PaginationRequest((pageNumber.Value - 1), pageSize.Value)
                    );

                    var result = await sender.Send(query);

                    var response = result.Adapt<GetDisksResponse>();
                    var apiResponse = new ApiResponseDto(
                        StatusCodes.Status200OK,
                        "Disks fetched successfully",
                        response
                    );

                    return Results.Ok(apiResponse);
                }
            )
            .RequireAuthorization()
            .WithName("GetDisks")
            .Produces<GetDisksResponse>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get disks in a server")
            .WithDescription("Get paginated list of disks installed in a server");
    }
}
