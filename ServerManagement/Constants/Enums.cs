using System.Text.Json.Serialization;

namespace ServerManagement.Constants;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ServerStatus
{
    Unknown,
    Running,
    Stopped,
    Maintenance,
    Decommissioned,
}

public enum ServerOperatingSystem
{
    Windows,
    Linux,
}

public enum DiskType
{
    Unknown,
    HDD,
    SSD,
    NVMe,
}
