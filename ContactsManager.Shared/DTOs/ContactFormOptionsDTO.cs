namespace ContactsManager.Shared.DTOs;

/// <summary>
/// All dictionary data required to create or edit a contact.
/// </summary>
public sealed class ContactFormOptionsDTO
{
    public IReadOnlyList<CategoryOptionDTO> Categories { get; set; } = [];
    public IReadOnlyList<LookupOptionDTO> Subcategories { get; set; } = [];
    public IReadOnlyList<LookupOptionDTO> Educations { get; set; } = [];
}
