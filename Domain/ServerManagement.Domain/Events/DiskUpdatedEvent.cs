namespace ServerManagement.Domain.Events;

public record DiskUpdatedEvent(Disk Disk) : IDomainEvent;
