namespace ServerManagement.API.Features.Server.AddServer;

public record AddServerRequest(
    string Name,
    bool IsOnline,
    Domain.Enums.OperationStatus Status,
    string HostName,
    string PrimaryIp,
    List<string> IpAddresses,
    string MacAddress,
    Domain.Enums.OperatingSystem OperatingSystem,
    string GeographicRegion,
    int CpuCores,
    double MemoryInGb,
    TimeSpan Uptime,
    DateTimeOffset LastSeen,
    DateTimeOffset? DecommissionedAt,
    decimal HealthScore,
    List<string> Tags,
    Dictionary<string, string> Metadata,
    Guid? OwnerId,
    string Notes
);

public record AddServerResponse(bool Success);

public class AddServerEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(
                "/server",
                async ([FromBody] AddServerRequest addServerRequest, ISender sender) =>
                {
                    var command = addServerRequest.Adapt<AddServerCommand>();
                    var result = await sender.Send(command);

                    var response = result.Adapt<AddServerResponse>();

                    if (!response.Success)
                        return Results.BadRequest(
                            new ApiResponseDto(
                                1,
                                "Failed to add server. Check the input for details.",
                                null
                            )
                        );

                    var apiResponse = new ApiResponseDto(0, "Server added successfully", response);
                    return Results.Ok(apiResponse);
                }
            )
            .RequireAuthorization()
            .WithName("AddServer")
            .Produces<AddServerResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Add Server")
            .WithDescription("Create a new server");
    }
}
