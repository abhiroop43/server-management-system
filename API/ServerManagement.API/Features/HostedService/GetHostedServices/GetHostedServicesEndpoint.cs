namespace ServerManagement.API.Features.HostedService.GetHostedServices;

public class GetHostedServicesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "/servers/{serverId:guid}/services",
                async (
                    [FromRoute] Guid serverId,
                    [FromQuery] int? pageNumber,
                    [FromQuery] int? pageSize,
                    ISender sender
                ) =>
                {
                    if (pageNumber is null or 0)
                    {
                        pageNumber = 1;
                    }

                    if (pageSize is null or 0)
                    {
                        pageSize = 10;
                    }

                    var query = new GetHostedServicesQuery(
                        serverId,
                        new PaginationRequest(pageNumber.Value - 1, pageSize.Value)
                    );

                    var result = await sender.Send(query);

                    var apiResponse = new ApiResponseDto(
                        StatusCodes.Status200OK,
                        "Hosted services fetched successfully",
                        result
                    );

                    return Results.Ok(apiResponse);
                }
            )
            .RequireAuthorization()
            .WithName("GetHostedServices")
            .Produces<ApiResponseDto>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Hosted Services")
            .WithDescription("Get Hosted Services for a Server using the Server Id");
    }
}
