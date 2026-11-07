using ContactsManager.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace ContactsManager.Api.Models;

public class Category
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = "";

    [Required]
    public SubcategoryMode SubcategoryMode { get; set; }

    public Category() { Id = Guid.NewGuid(); }
}
