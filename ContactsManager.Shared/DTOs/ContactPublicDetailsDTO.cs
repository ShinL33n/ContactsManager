namespace ContactsManager.Shared.DTOs;

/// <summary>
/// DTO used for viewing basic public contact information.
/// </summary>
/// <remarks>
/// It contains only the basic fields needed for displaying a contact in a home page list by unuthorized users.
/// </remarks>
public class ContactPublicDetailsDTO
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";

    public string Email { get; set; } = "";

    public string? PhoneNumber { get; set; } = "";

    public uint? Age { get; set; }

    public string CategoryName { get; set; } = "";
    public string? SubcategoryName { get; set; } = null;
    public string? CustomSubcategoryName { get; set; } = null;

}