namespace ServerManagement.Domain.Abstractions;

public interface IAggregate
{
    IReadOnlyList<IDomainEvent> DomainEvents { get; }
    IDomainEvent[] ClearEvents();
    void AddDomainEvent(IDomainEvent domainEvent);
}

public interface IAggregate<T> : IAggregate, IEntity<T> { }
