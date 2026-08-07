namespace ServerManagement.API.Features.HostedService.GetHostedServices;

public record GetHostedServicesQuery(Guid ServerId, PaginationRequest PaginationRequest)
    : IQuery<GetHostedServicesResult>;

public record GetHostedServicesResult(PaginationResult<HostedServiceDto> HostedServices);

public class GetHostedServicesHandler(ApplicationDbContext dbContext)
    : IQueryHandler<GetHostedServicesQuery, GetHostedServicesResult>
{
    public async Task<GetHostedServicesResult> Handle(
        GetHostedServicesQuery query,
        CancellationToken cancellationToken
    )
    {
        var totalServices = await dbContext
            .HostedServices.AsNoTracking()
            .LongCountAsync(cancellationToken);

        var hostedServices = await dbContext
            .HostedServices.AsNoTracking()
            .Where(x => x.ServerId == ServerId.Of(query.ServerId))
            .OrderByDescending(x => x.UpdatedDate)
            .Skip(query.PaginationRequest.PageIndex * query.PaginationRequest.PageSize)
            .Take(query.PaginationRequest.PageSize)
            .ToListAsync(cancellationToken);

        var servicesDto = hostedServices.Adapt<List<HostedServiceDto>>();

        var result = new PaginationResult<HostedServiceDto>(
            query.PaginationRequest.PageIndex + 1,
            query.PaginationRequest.PageSize,
            totalServices,
            servicesDto
        );

        return new GetHostedServicesResult(result);
    }
}
