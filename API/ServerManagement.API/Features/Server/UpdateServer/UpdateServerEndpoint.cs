namespace ServerManagement.API.Features.Server.UpdateServer;

public record UpdateServerRequest(
    Guid Id,
    string Name,
    string HostName,
    string PrimaryIpAddress,
    int CpuCores,
    double MemoryInGb,
    string Status,
    Guid? OwnerId,
    List<string>? Tags,
    Dictionary<string, string>? Metadata,
    List<string>? IpAddresses,
    string GeographicRegion
);

public record UpdateServerResponse(bool Success);

public class UpdateServerEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut(
                "/servers",
                async ([FromBody] UpdateServerRequest request, ISender sender) =>
                {
                    var command = request.Adapt<UpdateServerCommand>();

                    var result = await sender.Send(command);

                    var response = result.Adapt<UpdateServerResponse>();

                    if (!response.Success)
                        return Results.BadRequest(
                            new ApiResponseDto(
                                1,
                                "Failed to update server. Check the input for details.",
                                null
                            )
                        );
                    var apiResponse = new ApiResponseDto(
                        StatusCodes.Status201Created,
                        "Server added successfully",
                        response
                    );
                    return Results.Ok(apiResponse);
                }
            )
            .RequireAuthorization()
            .WithName("UpdateServer")
            .Produces<ApiResponseDto>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update a server")
            .WithDescription("Update an existing server using the server's Id");
    }
}
