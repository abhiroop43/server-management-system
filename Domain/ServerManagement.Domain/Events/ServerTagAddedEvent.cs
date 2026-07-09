namespace ServerManagement.Domain.Events;

public record ServerTagAddedEvent(Server Server) : IDomainEvent;
