using Microsoft.AspNetCore.Identity;

namespace ServerManagement.Infrastructure.Auth;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime? DateOfBirth { get; set; }
}
