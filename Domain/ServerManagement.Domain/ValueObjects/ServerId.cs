using ServerManagement.Domain.Exceptions;

namespace ServerManagement.Domain.ValueObjects;

public record ServerId
{
    private ServerId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static ServerId Of(Guid value)
    {
        return value == Guid.Empty
            ? throw new DomainException("Server Id cannot be empty")
            : new ServerId(value);
    }
}
