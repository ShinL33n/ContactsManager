using System.ComponentModel.DataAnnotations;

namespace ContactsManager.Shared.DTOs;

public abstract class ContactUpsertBaseDTO
{
    [Required]
    public string FirstName { get; set; } = "";

    [Required]
    public string LastName { get; set; } = "";

    [Required] [EmailAddress]
    public string Email { get; set; } = "";

    public string? PhoneNumber { get; set; }
    public DateOnly? Birthday { get; set; }
    public uint? Age { get; set; }
    public string? Address { get; set; }
    public string? JobTitle { get; set; }
    public string? Notes { get; set; }

    [Required]
    public Guid CategoryId { get; set; }
    public Guid? SubcategoryId { get; set; }
    public Guid? CustomSubcategoryId { get; set; }
    public string? CustomSubcategoryName { get; set; }

    public Guid? EducationId { get; set;}
}
