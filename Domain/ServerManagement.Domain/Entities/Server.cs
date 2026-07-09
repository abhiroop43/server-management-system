namespace ServerManagement.Domain.Entities;

public class Server : Aggregate<ServerId>
{
    private readonly List<Disk> _disks = [];

    private readonly List<HostedService> _hostedServices = [];
    public ServerName Name { get; private set; } = null!;
    public bool IsOnline { get; private set; } = true;
    public OperationStatus Status { get; private set; } = OperationStatus.Running;
    public HostName HostName { get; private set; } = null!;
    public PrimaryIpAddress PrimaryIpAddress { get; private set; } = null!;
    public List<string> IpAddresses { get; private set; } = [];
    public int CpuCores { get; private set; }
    public double MemoryInGb { get; private set; }
    public IReadOnlyList<Disk> Disks => _disks.AsReadOnly();
    public TimeSpan UpTime { get; private set; }
    public DateTimeOffset LastSeen { get; private set; } = DateTime.Now;

    public DateTimeOffset? DecommissionedAt { get; private set; }

    public decimal HealthScore { get; private set; }

    public List<string> Tags { get; private set; } = [];

    public Dictionary<string, string> Metadata { get; private set; } = null!;

    public Guid? OwnerId { get; private set; }
    public IReadOnlyList<HostedService> Services => _hostedServices.AsReadOnly();

    public string? Notes { get; private set; }

    public static Server Create(
        ServerId serverId,
        Guid? ownerId,
        ServerName name,
        HostName hostName,
        PrimaryIpAddress primaryIpAddress,
        int cpuCores,
        double memoryInGb,
        string? notes,
        OperationStatus operationStatus,
        List<string> tags,
        Dictionary<string, string> metadata,
        List<string> ipAddresses
    )
    {
        var server = new Server
        {
            Id = serverId,
            OwnerId = ownerId,
            Name = name,
            HostName = hostName,
            PrimaryIpAddress = primaryIpAddress,
            CpuCores = cpuCores,
            MemoryInGb = memoryInGb,
            Notes = notes,
            Status = operationStatus,
            Tags = tags,
            Metadata = metadata,
            IpAddresses = ipAddresses,
        };

        server.AddDomainEvent(new ServerCreatedEvent(server));

        return server;
    }

    public void Update(
        ServerName name,
        HostName hostName,
        PrimaryIpAddress primaryIpAddress,
        int cpuCores,
        double memoryInGb,
        Guid? ownerId
    )
    {
        Name = name;
        HostName = hostName;
        PrimaryIpAddress = primaryIpAddress;
        CpuCores = cpuCores;
        MemoryInGb = memoryInGb;
        OwnerId = ownerId;

        AddDomainEvent(new ServerUpdatedEvent(this));
    }

    public void UpdateHealth(decimal healthScore)
    {
        HealthScore = healthScore;
    }

    public void UpdateUpTime()
    {
        if (Status != OperationStatus.Running)
            return;
        UpTime = (DateTime.Now - CreatedDate)!.Value;
    }

    public void AddDisk(Disk disk)
    {
        _disks.Add(disk);

        AddDomainEvent(new DiskAssignedToServerEvent(disk, this));
    }

    public void RemoveDisk(Disk disk)
    {
        _disks.Remove(disk);

        AddDomainEvent(new DiskRemovedFromServerEvent(disk, this));
    }

    public void AddHostedService(HostedService hostedService)
    {
        _hostedServices.Add(hostedService);

        AddDomainEvent(new ServiceCreatedOnServerEvent(hostedService, this));
    }

    public void RemoveHostedService(HostedService hostedService)
    {
        _hostedServices.Remove(hostedService);

        AddDomainEvent(new ServiceRemovedFromServerEvent(hostedService, this));
    }

    public void AddServerTag(string tag)
    {
        Tags.Add(tag);

        AddDomainEvent(new ServerTagAddedEvent(this));
    }

    public void RemoveServerTag(string tag)
    {
        Tags.Remove(tag);

        AddDomainEvent(new ServerTagRemovedEvent(this));
    }

    public void DecommissionServer()
    {
        Status = OperationStatus.Decommissioned;
        DecommissionedAt = DateTimeOffset.Now;

        AddDomainEvent(new ServerDecommissionedEvent(this));
    }
}
