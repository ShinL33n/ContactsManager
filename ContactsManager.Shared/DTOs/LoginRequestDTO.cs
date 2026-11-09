using System.ComponentModel.DataAnnotations;

namespace ContactsManager.Shared.DTOs;

public class LoginRequestDTO
{
    [Required] [EmailAddress]
    [Display(Name = "Email Address")]
    public string Email { get; set; } = "";

    [Required]
    public string Password { get; set; } = "";
}
