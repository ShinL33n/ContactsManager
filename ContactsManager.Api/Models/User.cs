using ContactsManager.Shared.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactsManager.Api.Models;

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
    public string Role { get; set; } = Roles.UserRole; // Default role is "User"

    public string? PhoneNumber { get; set; }

    public DateOnly? Birthday { get; set; }

    public Guid CategoryId { get; set; }
    [ForeignKey(nameof(CategoryId))]
    public string CategoryName { get; set; } = Categories.Private;  // Default category is "Private"

    public Guid? SubcategoryId { get; set; }
    [ForeignKey(nameof(SubcategoryId))]
    public string? SubcategoryName { get; set; }

    public Guid? CustomSubcategoryId { get; set; }
    [ForeignKey(nameof(CustomSubcategoryId))]
    public string? CustomSubcategoryName { get; set; }




    
}
