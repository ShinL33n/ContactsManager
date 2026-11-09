using System.ComponentModel.DataAnnotations;

namespace ContactsManager.Shared.DTOs;

public class RegisterRequestDTO
{
    [Required]
    public string FirstName { get; set; } = "";

    [Required]
    public string LastName { get; set; } = "";

    [Required]
    [EmailAddress]
    [Display(Name = "Email Address")]
    public string Email { get; set; } = "";

    [Required]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "Hasło musi mieć min. 8 znaków.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).+$",
                       ErrorMessage = "Hasło musi zawierać małą i wielką literę, cyfrę oraz znak specjalny.")]
    public string Password { get; set; } = "";

    [Required]
    [Compare("Password", ErrorMessage = "Hasła nie są zgodne.")]
    public string ConfirmPassword { get; set; } = "";
}
