namespace ServerManagement.Domain.ValueObjects;

public record GeographicRegion
{
    private GeographicRegion(string value) => Value = value;

    public string Value { get; } = null!;

    public static GeographicRegion Of(string value)
    {
        var allowedRegions = new List<string>
        {
            "US-EAST",
            "US-WEST",
            "UK-NORTH",
            "UK-SOUTH",
            "INDIA-CENTRAL",
            "INDIA-SOUTH",
        };
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("Geographic Region must be provided");
        }

        if (!allowedRegions.Contains(value))
        {
            throw new DomainException("Invalid Geographic Region provided");
        }

        return new GeographicRegion(value);
    }
}
