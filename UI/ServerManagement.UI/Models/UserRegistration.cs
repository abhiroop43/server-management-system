using System.ComponentModel.DataAnnotations;

namespace ServerManagement.UI.Models;

public class UserRegistration
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    [Required]
    [MinLength(8)]
    [MaxLength(15)]
    public string? Password { get; set; }

    public DateTime? DateOfBirth { get; set; }
}
