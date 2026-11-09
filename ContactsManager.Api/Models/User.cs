using ContactsManager.Shared.Utils;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactsManager.Api.Models;

[Index(nameof(Email), IsUnique = true)]
public class User
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string FirstName { get; set; } = "";

    [Required]
    public string LastName { get; set; } = "";

    [Required]
    public string Email { get; set; } = "";

    [Required]
    public string PasswordHash { get; set; } = "";

    [Required]
    public string Role { get; set; } = RoleNames.UserRole; // Default role is "User"

    public string? PhoneNumber { get; set; }
    public DateOnly? Birthday { get; set; }
    public uint? Age { get; set; }
    public string? Address { get; set; }
    public string? JobTitle { get; set; }
    public string? Notes { get; set; }

    [Required]
    public Guid CategoryId { get; set; }
    [ForeignKey(nameof(CategoryId))]
    public required Category Category { get; set; } 

    public Guid? SubcategoryId { get; set; }
    [ForeignKey(nameof(SubcategoryId))]
    public Subcategory? Subcategory { get; set; }

    public Guid? CustomSubcategoryId { get; set; }
    [ForeignKey(nameof(CustomSubcategoryId))]
    public CustomSubcategory? CustomSubcategory { get; set; }
    
    public Guid? EducationId { get; set; }
    [ForeignKey(nameof(EducationId))]
    public Education? Education { get; set; }


    public User() { Id = Guid.NewGuid(); }


}
