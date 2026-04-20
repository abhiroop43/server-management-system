using System.ComponentModel.DataAnnotations;

namespace ServerManagement.Models;

public class ServerHostedService
{
    [Key]
    [Required]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(128)]
    public string ServiceName { get; set; } = null!;

    [Required]
    public int Port { get; set; }

    public bool IsListening { get; set; } = true;
    public DateTimeOffset LastChecked { get; set; } = DateTime.Now;
}
