namespace ServerManagement.API.Features.Server.GetServerDetails;

public record GetServerDetailsQuery(Guid ServerId) : IQuery<GetServerDetailsResult>;

public record GetServerDetailsResult();

public class GetServerDetailsQueryHandler(ApplicationDbContext dbContext)
    : IQueryHandler<GetServerDetailsQuery, GetServerDetailsResult>
{
    public Task<GetServerDetailsResult> Handle(
        GetServerDetailsQuery request,
        CancellationToken cancellationToken
    )
    {
        throw new NotImplementedException();
    }
}
