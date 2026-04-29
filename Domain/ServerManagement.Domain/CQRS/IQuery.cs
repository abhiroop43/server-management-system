namespace ServerManagement.Domain.CQRS;

public interface IQuery<TResponse> : IRequest<TResponse>
    where TResponse : notnull { }
