using Microsoft.EntityFrameworkCore;
using ServerManagement.API.Dtos;
using ServerManagement.Domain.Pagination;
using ServerManagement.Infrastructure.Data;

namespace ServerManagement.API.Features.Disk.GetDisks;

public record GetDisksQuery(PaginationRequest PaginationRequest) : IQuery<GetDisksResult>;

public record GetDisksResult(PaginationResult<DiskDto> Disks);

public class GetDisksQueryHandler(ApplicationDbContext dbContext)
    : IQueryHandler<GetDisksQuery, GetDisksResult>
{
    public async Task<GetDisksResult> Handle(
        GetDisksQuery query,
        CancellationToken cancellationToken
    )
    {
        var totalDisks = await dbContext.Disks.LongCountAsync(cancellationToken);
        var disks = await dbContext
            .Disks.AsNoTracking()
            .OrderByDescending(d => d.UpdatedDate)
            .Skip(query.PaginationRequest.PageIndex * query.PaginationRequest.PageSize)
            .Take(query.PaginationRequest.PageSize)
            .ToListAsync(cancellationToken);

        var disksDto = disks.Adapt<List<DiskDto>>();

        var result = new PaginationResult<DiskDto>(
            query.PaginationRequest.PageIndex,
            query.PaginationRequest.PageSize,
            totalDisks,
            disksDto
        );

        return new GetDisksResult(result);
    }
}
