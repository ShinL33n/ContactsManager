using System.ComponentModel.DataAnnotations;

namespace ContactsManager.Api.Models;

public class Subcategory
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = "";

    public Subcategory() { Id = Guid.NewGuid(); }
}
