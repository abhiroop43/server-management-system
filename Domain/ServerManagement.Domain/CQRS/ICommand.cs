namespace ServerManagement.Domain.CQRS;

public interface ICommand : IRequest<Unit> { }

public interface ICommand<TResponse> : IRequest<TResponse> { }
