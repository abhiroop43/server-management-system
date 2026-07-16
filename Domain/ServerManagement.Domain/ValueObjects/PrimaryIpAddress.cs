using System.Text.RegularExpressions;

namespace ServerManagement.Domain.ValueObjects;

public record PrimaryIpAddress
{
    private PrimaryIpAddress(string value) => Value = value;

    public string Value { get; } = null!;

    public static PrimaryIpAddress Of(string value)
    {
        return !Regex.IsMatch(value, @"^((25[0-5]|(2[0-4]|1\d|[1-9]|)\d)\.?\b){4}$")
            ? throw new DomainException("Primary IP Address is not in a valid format")
            : new PrimaryIpAddress(value);
    }
}
