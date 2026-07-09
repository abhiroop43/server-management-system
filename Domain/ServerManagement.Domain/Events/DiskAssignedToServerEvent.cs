namespace ServerManagement.Domain.Events;

public record DiskAssignedToServerEvent(Disk Disk, Server Server) : IDomainEvent;
