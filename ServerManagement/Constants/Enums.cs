namespace ServerManagement.Constants;

public enum ServerStatus
{
    Unknown,
    Running,
    Stopped,
    Maintenance,
    Decommissioned,
}

public enum OperatingSystem
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
