using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;    

namespace ContactsManager.Api.Models;

[Index(nameof(Name), IsUnique = true)]
public class CustomSubcategory
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = "";

    public CustomSubcategory() { Id = Guid.NewGuid(); }
}
