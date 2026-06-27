namespace ServerManagement.Domain.ValueObjects;

public record HostedServiceName
{
    private const int MinLength = 3;
    private const int MaxLength = 128;

    private HostedServiceName(string value) => Value = value;

    public string? Value { get; } = null!;

    public static HostedServiceName Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("Service Name cannot be null");
        }

        if (value.Length < MinLength)
        {
            throw new DomainException($"Service Name must be at least {MinLength} characters");
        }

        if (value.Length > MaxLength)
        {
            throw new DomainException($"Service name can be at most {MaxLength} characters");
        }

        return new HostedServiceName(value);
    }
}
