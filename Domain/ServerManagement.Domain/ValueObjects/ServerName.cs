namespace ServerManagement.Domain.ValueObjects;

public record ServerName
{
    private const int MinLength = 3;
    private const int MaxLength = 128;

    private ServerName(string value) => Value = value;

    public string Value { get; } = null!;

    public static ServerName Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("Server Name must be provided");
        }

        if (value.Length is < MinLength or > MaxLength)
        {
            throw new DomainException(
                $"Please provide a server name between {MinLength} and {MaxLength} characters"
            );
        }

        return new ServerName(value);
    }
}
