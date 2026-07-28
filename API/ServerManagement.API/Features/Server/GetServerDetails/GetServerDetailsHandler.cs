namespace ServerManagement.API.Features.Server.GetServerDetails;

public record GetServerDetailsQuery(Guid ServerId) : IQuery<GetServerDetailsResult>;

public record GetServerDetailsResult(
    Guid Id,
    string Name,
    bool IsOnline,
    string Status,
    string HostName,
    string PrimaryIpAddress,
    string OperatingSystem,
    List<string> IpAddrIpAddresses,
    int CpuCores,
    double MemoryInGb,
    string UpTime,
    DateTimeOffset LastSeen,
    DateTimeOffset? DecommissionedAt,
    decimal HealthScore,
    string GeographicRegion,
    List<string> Tags,
    Dictionary<string, string> Metadata,
    Guid? OwnerId,
    string? CreatedBy,
    DateTime? CreatedDate,
    string? UpdatedBy,
    DateTime? UpdatedDate
);

public class GetServerDetailsQueryHandler(ApplicationDbContext dbContext)
    : IQueryHandler<GetServerDetailsQuery, GetServerDetailsResult>
{
    public async Task<GetServerDetailsResult> Handle(
        GetServerDetailsQuery query,
        CancellationToken cancellationToken
    )
    {
        var serverInDb = await dbContext
            .Servers.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == ServerId.Of(query.ServerId), cancellationToken);
        return serverInDb == null
            ? throw new NotFoundException(nameof(serverInDb), query.ServerId)
            : serverInDb.Adapt<GetServerDetailsResult>();
    }
}
