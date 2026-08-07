namespace ServerManagement.API.Features.Server.GetServers;

public record GetServersQuery(PaginationRequest PaginationRequest) : IQuery<GetServersResult>;

public record GetServersResult(PaginationResult<ServerDto> Servers);

public class GetServersQueryHandler(ApplicationDbContext dbContext)
    : IQueryHandler<GetServersQuery, GetServersResult>
{
    public async Task<GetServersResult> Handle(
        GetServersQuery query,
        CancellationToken cancellationToken
    )
    {
        var totalServers = await dbContext.Servers.AsNoTracking().LongCountAsync(cancellationToken);

        var servers = await dbContext
            .Servers.OrderByDescending(x => x.UpdatedDate)
            .Skip(query.PaginationRequest.PageIndex * query.PaginationRequest.PageSize)
            .Take(query.PaginationRequest.PageSize)
            .ToListAsync(cancellationToken);

        var serversDto = servers.Adapt<List<ServerDto>>();

        var result = new PaginationResult<ServerDto>(
            (query.PaginationRequest.PageIndex + 1),
            query.PaginationRequest.PageSize,
            totalServers,
            serversDto
        );

        return new GetServersResult(result);
    }
}
