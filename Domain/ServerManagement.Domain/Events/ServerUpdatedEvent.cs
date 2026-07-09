namespace ServerManagement.Domain.Events;

public record ServerUpdatedEvent(Server Server) : IDomainEvent;
