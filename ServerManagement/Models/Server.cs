using ServerManagement.Constants;
using OperatingSystem = System.OperatingSystem;

namespace ServerManagement.Models;

public class Server
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public bool IsOnline { get; set; }
    public ServerStatus Status { get; set; }
    public string HostName { get; set; } = null!;
    public string PrimaryIp { get; set; } = null!;
    public List<string> IpAddresses { get; set; } = null!;
    public string MacAddress { get; set; } = null!;
    public OperatingSystem OperatingSystem { get; set; } = null!;
    public string GeographicRegion { get; set; } = null!;
    public int CpuCores { get; set; }
    public double MemoryInGb { get; set; }
    public List<Disk> Disks { get; set; } = null!;
    public TimeSpan UpTime { get; set; }
    public DateTimeOffset LastSeen { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? DecommissionedAt { get; set; }
    public decimal HealthScore { get; set; }
    public List<string> Tags { get; set; } = null!;
    public Dictionary<string, string> Metadata { get; set; } = null!;
    public Guid? OwnerId { get; set; }
    public List<ServiceStatus> Services { get; set; } = null!;
    public string Notes { get; set; } = null!;
}
