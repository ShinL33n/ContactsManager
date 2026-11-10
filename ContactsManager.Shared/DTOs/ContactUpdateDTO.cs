using System.ComponentModel.DataAnnotations;

namespace ContactsManager.Shared.DTOs;

public class ContactUpdateDTO : ContactUpsertBaseDTO
{
    [Required]
    public Guid Id { get; set; }

    [StringLength(100, MinimumLength = 8,
                       ErrorMessage = "Hasło musi mieć min. 8 znaków.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).+$",
                       ErrorMessage = "Hasło musi zawierać małą i wielką literę, cyfrę oraz znak specjalny.")]
    public string? Password { get; set; }

    [Compare("Password", ErrorMessage = "Hasła nie są zgodne.")]
    public string? ConfirmPassword { get; set; }
}
