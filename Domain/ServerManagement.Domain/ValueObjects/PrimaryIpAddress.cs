namespace ServerManagement.Domain.ValueObjects;

public record PrimaryIpAddress
{
    private PrimaryIpAddress(string value) => Value = value;

    public string Value { get; } = null!;
}
