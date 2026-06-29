using System.Text.RegularExpressions;

namespace ServerManagement.Domain.ValueObjects;

public record MacAddress
{
    private MacAddress(string value) => Value = value;

    public string Value { get; } = null!;

    public static MacAddress Of(string value)
    {
        return !Regex.IsMatch(
            value,
            @"^(([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})|([0-9A-Fa-f]{4}\.){2}([0-9A-Fa-f]{4}))$"
        )
            ? throw new DomainException("MAC Address is not in a valid format")
            : new MacAddress(value);
    }
}
