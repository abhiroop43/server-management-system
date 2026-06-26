namespace ServerManagement.Domain.ValueObjects;

public record HostedServiceId
{
    private HostedServiceId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static HostedServiceId Of(Guid value)
    {
        return value == Guid.Empty
            ? throw new DomainException("Hosted Service Id cannot be empty")
            : new HostedServiceId(value);
    }
};
