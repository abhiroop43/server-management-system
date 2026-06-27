namespace ServerManagement.Domain.Events;

public record ServiceRemovedEvent(HostedService Service) : IDomainEvent;
