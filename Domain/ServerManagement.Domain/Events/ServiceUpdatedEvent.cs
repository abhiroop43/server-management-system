namespace ServerManagement.Domain.Events;

public record ServiceUpdatedEvent(HostedService Service) : IDomainEvent;
