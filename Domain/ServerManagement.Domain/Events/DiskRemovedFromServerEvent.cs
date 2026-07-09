namespace ServerManagement.Domain.Events;

public record DiskRemovedFromServerEvent(Disk Disk, Server Server) : IDomainEvent;
