namespace ServerManagement.Domain.Events;

public record ServiceCreatedOnServerEvent(HostedService HostedService, Server Server)
    : IDomainEvent;
