using System.ComponentModel.DataAnnotations;

namespace ServerManagement.UI.Models;

public class UserLogin
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
    [MaxLength(15, ErrorMessage = "Password must be at most 15 characters long.")]
    public string? Password { get; set; }
}
