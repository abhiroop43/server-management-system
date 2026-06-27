namespace ServerManagement.Domain.Events;

public record DiskRemovedEvent(Disk Disk) : IDomainEvent;
