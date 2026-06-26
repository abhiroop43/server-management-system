namespace ServerManagement.Domain.ValueObjects;

public record DiskId
{
    private DiskId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static DiskId Of(Guid value)
    {
        return value == Guid.Empty
            ? throw new DomainException("Disk Id cannot be empty")
            : new DiskId(value);
    }
}
