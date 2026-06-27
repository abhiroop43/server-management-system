namespace ServerManagement.Domain.ValueObjects;

public record DiskName
{
    private const int MinLength = 3;
    private const int MaxLength = 128;

    private DiskName(string value) => Value = value;

    public string Value { get; } = null!;

    public static DiskName Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("Disk Name cannot be null or empty");

        if (value.Length < MinLength)
            throw new DomainException($"Disk Name must be at least {MinLength} characters long");

        if (value.Length > MaxLength)
            throw new DomainException($"Disk Name must be at most {MaxLength} character long");

        return new DiskName(value);
    }
}
