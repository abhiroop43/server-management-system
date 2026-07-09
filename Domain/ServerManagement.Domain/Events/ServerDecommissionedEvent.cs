namespace ServerManagement.Domain.Events;

public record ServerDecommissionedEvent(Server Server) : IDomainEvent;
