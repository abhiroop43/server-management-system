namespace ServerManagement.Domain.ValueObjects;

public record HostName
{
    private const int MinLength = 3;
    private const int MaxLength = 128;

    private HostName(string value) => Value = value;

    public string Value { get; } = null!;

    public static HostName Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("Host Name must be provided for the server");
        }

        if (value.Length is < MinLength or > MaxLength)
        {
            throw new DomainException(
                $"Host Name must be between {MinLength} and {MaxLength} characters"
            );
        }

        return new HostName(value);
    }
}
