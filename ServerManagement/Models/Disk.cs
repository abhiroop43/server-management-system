using ServerManagement.Constants;

namespace ServerManagement.Models;

public class Disk
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public long CapacityGb { get; set; }
    public long UsedGb { get; set; }
    public DiskType DiskType { get; set; }
}
