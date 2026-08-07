namespace ServerManagement.API.Features.Server.GetServers;

public record GetServersResponse(PaginationResult<ServerDto> Servers);

public class GetServersEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "/servers",
                async ([FromQuery] int? pageNumber, [FromQuery] int? pageSize, ISender sender) =>
                {
                    if (pageNumber is null or 0)
                    {
                        pageNumber = 1;
                    }

                    if (pageSize is null or 0)
                    {
                        pageSize = 10;
                    }

                    var query = new GetServersQuery(
                        new PaginationRequest(pageNumber.Value - 1, pageSize.Value)
                    );
                    var result = await sender.Send(query);

                    var response = result.Adapt<GetServersResponse>();

                    var apiResponse = new ApiResponseDto(
                        StatusCodes.Status200OK,
                        "Servers fetched successfully",
                        response
                    );

                    return Results.Ok(apiResponse);
                }
            )
            .RequireAuthorization()
            .WithName("GetServers")
            .Produces<ApiResponseDto>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get List of Servers")
            .WithDescription("Retrieve a paginated list of all available servers");
    }
}
