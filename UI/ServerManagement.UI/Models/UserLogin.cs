using System.ComponentModel.DataAnnotations;

namespace ServerManagement.UI.Models;

public class UserLogin
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    [MinLength(8)]
    [MaxLength(15)]
    public string? Password { get; set; }
}
