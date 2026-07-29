namespace ServerManagement.API.Features.Server.GetServerDetails;

public record GetServerDetailsResponse(
    Guid Id,
    string Name,
    bool IsOnline,
    string Status,
    string HostName,
    string PrimaryIpAddress,
    string OperatingSystem,
    List<string> IpAddresses,
    int CpuCores,
    double MemoryInGb,
    string UpTime,
    DateTimeOffset LastSeen,
    DateTimeOffset? DecommissionedAt,
    decimal HealthScore,
    string GeographicRegion,
    List<string> Tags,
    Dictionary<string, string> Metadata,
    Guid? OwnerId
);

public class GetServerDetailsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "/servers/{serverId:guid}",
                async ([FromRoute] Guid serverId, ISender sender) =>
                {
                    var query = new GetServerDetailsQuery(serverId);
                    var result = await sender.Send(query);
                    var response = result.Adapt<GetServerDetailsResponse>();
                    var apiResponse = new ApiResponseDto(
                        StatusCodes.Status200OK,
                        "Server retrieved successfully",
                        response
                    );
                    return Results.Ok(apiResponse);
                }
            )
            .RequireAuthorization()
            .WithName("GetServerDetails")
            .Produces<ApiResponseDto>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Server Details")
            .WithDescription("Get details of a server using the Server Id");
    }
}
