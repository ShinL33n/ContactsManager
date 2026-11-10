using ContactsManager.Shared.Enums;

namespace ContactsManager.Shared.DTOs;

/// <summary>
/// Category metadata used by the contact form to decide which subcategory input to display.
/// </summary>
public sealed class CategoryOptionDTO : LookupOptionDTO
{
    public SubcategoryMode SubcategoryMode { get; set; }
}
