using System.ComponentModel.DataAnnotations;

namespace ContactsManager.Api.Models;

public class Education
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = "";

    public Education() { Id = Guid.NewGuid(); }
}
