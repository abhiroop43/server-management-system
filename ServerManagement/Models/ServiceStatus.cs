namespace ServerManagement.Models;

public class ServiceStatus
{
    public string ServiceName { get; set; } = null!;
    public int Port { get; set; }
    public bool IsListening { get; set; }
    public DateTimeOffset LastChecked { get; set; }
}
