using System.ComponentModel.DataAnnotations;
using ServerManagement.Constants;

namespace ServerManagement.Models;

public class Disk
{
    [Key]
    [Required]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(128)]
    public string Name { get; set; } = null!;

    [Required]
    public long CapacityGb { get; set; }

    [Required]
    public long UsedGb { get; set; }

    public DiskType DiskType { get; set; }
}
