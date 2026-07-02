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
    public int CpuCores { get; set; }
    public double MemoryInGb { get; set; }
    public IReadOnlyList<Disk> Disks => _disks.AsReadOnly();
    public TimeSpan UpTime { get; set; }
    public DateTimeOffset LastSeen { get; set; } = DateTime.Now;

    public DateTimeOffset? DecommissionedAt { get; set; }

    public decimal HealthScore { get; set; }

    public List<string> Tags { get; set; } = [];

    public Dictionary<string, string> Metadata { get; set; } = null!;

    public Guid? OwnerId { get; set; }
    public IReadOnlyList<HostedService> Services => _hostedServices.AsReadOnly();

    public string Notes { get; set; } = null!;

    public static Server Create(
        ServerId serverId,
        Guid? ownerId,
        ServerName name,
        HostName hostName,
        PrimaryIpAddress primaryIpAddress,
        int cpuCores,
        double memoryInGb
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
        };

        // generate domain event

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

        // generate domain event
    }

    public void UpdateHealth(decimal healthScore)
    {
        this.HealthScore = healthScore;
    }

    public void AddDisk(Disk disk)
    {
        _disks.Add(disk);

        // generate domain event
    }

    public void RemoveDisk(Disk disk)
    {
        _disks.Remove(disk);

        // generate domain event
    }

    public void AddHostedService(HostedService hostedService)
    {
        _hostedServices.Add(hostedService);

        // generate domain event
    }

    public void RemoveHostedService(HostedService hostedService)
    {
        _hostedServices.Remove(hostedService);

        // generate domain event
    }

    public void AddServerTag(string tag)
    {
        Tags.Add(tag);

        // generate domain event
    }

    public void RemoveServerTag(string tag)
    {
        Tags.Remove(tag);

        // generate domain event
    }

    public void DecommissionServer()
    {
        Status = OperationStatus.Decommissioned;
        DecommissionedAt = DateTimeOffset.Now;

        // generate domain event
    }
}
