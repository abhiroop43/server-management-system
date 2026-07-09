namespace ServerManagement.Domain.Events;

public record ServerTagRemovedEvent(Server Server) : IDomainEvent;
