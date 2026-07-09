namespace ServerManagement.Domain.Events;

public record ServiceRemovedFromServerEvent(HostedService HostedService, Server Server)
    : IDomainEvent;
