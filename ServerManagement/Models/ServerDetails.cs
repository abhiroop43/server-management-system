using System.ComponentModel.DataAnnotations;
using ServerManagement.Constants;
using ServerManagement.Extensions;

namespace ServerManagement.Models;

public class ServerDetails
{
    [Key]
    [Required]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(128)]
    public string Name { get; set; } = null!;

    public bool IsOnline { get; set; } = true;
    public ServerStatus Status { get; set; }

    [Required]
    [MaxLength(128)]
    public string HostName { get; set; } = null!;

    [Required]
    [RegularExpression(@"^((25[0-5]|(2[0-4]|1\d|[1-9]|)\d)\.?\b){4}$")]
    public string PrimaryIp { get; set; } = null!;

    [RegularExpressionList(@"^((25[0-5]|(2[0-4]|1\d|[1-9]|)\d)\.?\b){4}$")]
    public List<string> IpAddresses { get; set; } = [];

    [RegularExpression(
        @"^(([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})|([0-9A-Fa-f]{4}\.){2}([0-9A-Fa-f]{4}))$"
    )]
    public string MacAddress { get; set; } = null!;

    public ServerOperatingSystem OperatingSystem { get; set; }

    [MaxLength(256)]
    public string GeographicRegion { get; set; } = null!;

    [Required]
    public int CpuCores { get; set; }

    [Required]
    public double MemoryInGb { get; set; }

    public List<Disk> Disks { get; set; } = null!;

    public TimeSpan UpTime { get; set; }

    public DateTimeOffset LastSeen { get; set; } = DateTime.Now;

    public DateTimeOffset CreatedAt { get; set; } = DateTime.Now;

    public DateTimeOffset? DecommissionedAt { get; set; }

    public decimal HealthScore { get; set; }

    public List<string> Tags { get; set; } = [];

    public Dictionary<string, string> Metadata { get; set; } = null!;

    public Guid? OwnerId { get; set; }

    public List<ServerHostedService> Services { get; set; } = null!;

    public string Notes { get; set; } = null!;
}
