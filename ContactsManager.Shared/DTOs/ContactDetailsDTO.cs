using ContactsManager.Shared.Utils;

namespace ContactsManager.Shared.DTOs;

/// <summary>
/// DTO used for viewing detailed contact information.
/// </summary>
/// <remarks>
/// It contains all the data fields needed to display a contact in the home page list for logged-in users.
/// </remarks>
public class ContactDetailsDTO : ContactPublicDTO
{
    public DateOnly? Birthday { get; set; }

    public string? Address { get; set; } = null;

    public string? JobTitle { get; set; } = null;

    public string? Education { get; set; } = null;

    public string? Notes { get; set; } = null;

    public string Role { get; set; } = RoleNames.UserRole; // Default role is user
}