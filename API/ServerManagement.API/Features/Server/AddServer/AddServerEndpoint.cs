namespace ServerManagement.API.Features.Server.AddServer;

public record AddServerRequest(
    string Name,
    string Status,
    string HostName,
    string PrimaryIp,
    List<string> IpAddresses,
    string MacAddress,
    string OperatingSystem,
    string GeographicRegion,
    int CpuCores,
    double MemoryInGb,
    List<string> Tags,
    Dictionary<string, string> Metadata,
    Guid? OwnerId,
    string Notes
);

public record AddServerResponse(Guid Id, bool Success);

public class AddServerEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(
                "/servers",
                async (AddServerRequest addServerRequest, ISender sender) =>
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
                    var apiResponse = new ApiResponseDto(
                        StatusCodes.Status201Created,
                        "Server added successfully",
                        response
                    );
                    return Results.Created($"/servers/{response.Id}", apiResponse);
                }
            )
            .RequireAuthorization()
            .WithName("AddServer")
            .Produces<ApiResponseDto>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Add Server")
            .WithDescription("Create a new server");
    }
}
