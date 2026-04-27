using ServerManagement.UI.Constants;

namespace ServerManagement.UI.Models
{
    public class ServerSummary
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsOnline { get; set; }
        public ServerStatus Status { get; set; }
        public string HostName { get; set; } = null!;
        public string PrimaryIp { get; set; } = null!;
    }
}
