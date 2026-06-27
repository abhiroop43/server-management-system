using ServerManagement.Domain.Events;

namespace ServerManagement.Domain.Entities;

public class Disk : Aggregate<DiskId>
{
    public ServerId ServerId { get; private set; } = null!;
    public DiskName Name { get; private set; } = null!;
    public long CapacityGb { get; private set; }
    public long UsedGb { get; private set; }
    public DiskType DiskType { get; private set; }

    public bool IsActive { get; private set; }

    public static Disk Add(
        DiskId id,
        ServerId serverId,
        DiskName diskName,
        long capacityGb,
        long usedGb,
        DiskType diskType
    )
    {
        if (capacityGb <= 0)
            throw new DomainException("Disk Capacity must be greater than 0");

        if (usedGb < 0)
            throw new DomainException("Used space cannot be less than 0");

        var disk = new Disk
        {
            ServerId = serverId,
            Id = id,
            Name = diskName,
            CapacityGb = capacityGb,
            UsedGb = usedGb,
            DiskType = diskType,
            IsActive = true,
        };

        disk.AddDomainEvent(new DiskCreatedEvent(disk));

        return disk;
    }

    public void Update(DiskName diskName, long capacityGb, long usedGb, DiskType diskType)
    {
        if (capacityGb <= 0)
            throw new DomainException("Disk Capacity must be greater than 0");

        if (usedGb < 0)
            throw new DomainException("Used space cannot be less than 0");

        Name = diskName;
        CapacityGb = capacityGb;
        UsedGb = usedGb;
        DiskType = diskType;
        IsActive = true;

        AddDomainEvent(new DiskUpdatedEvent(this));
    }

    public void Remove()
    {
        IsActive = false;
        AddDomainEvent(new DiskRemovedEvent(this));
    }
}
