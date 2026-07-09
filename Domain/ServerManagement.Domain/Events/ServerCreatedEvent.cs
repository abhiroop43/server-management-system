namespace ServerManagement.Domain.Events;

public record ServerCreatedEvent(Server Server) : IDomainEvent;
