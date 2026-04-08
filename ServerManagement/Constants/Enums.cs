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

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ServerOperatingSystem
{
    None,
    Windows,
    Linux,
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DiskType
{
    Unknown,
    HDD,
    SSD,
    NVMe,
}
