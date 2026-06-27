namespace ServerManagement.Domain.Events;

public record DiskCreatedEvent(Disk Disk) : IDomainEvent;
