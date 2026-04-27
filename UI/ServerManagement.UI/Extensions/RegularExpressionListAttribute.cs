using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ServerManagement.UI.Extensions;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class RegularExpressionListAttribute(string pattern) : RegularExpressionAttribute(pattern)
{
    public override bool IsValid(object? value)
    {
        return value is IEnumerable<string> enumerable
            && enumerable.All(val => Regex.IsMatch(val, Pattern));
    }
}
