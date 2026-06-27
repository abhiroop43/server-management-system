namespace ServerManagement.Domain.Events;

public record ServiceCreatedEvent(HostedService Service) : IDomainEvent;
