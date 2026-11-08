namespace ContactsManager.Shared.DTOs;

/// <summary>
/// DTO used for viewing detailed contact information.
/// </summary>
/// <remarks>
/// It contains all the data fields needed to display a contact in the home page list for logged-in users.
/// </remarks>
public class ContactDetailsDTO
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";

    public string Email { get; set; } = "";

    public string? PhoneNumber { get; set; } = "";

    public string CategoryName { get; set; } = "";
    public string? SubcategoryName { get; set; } = null;
    public string? CustomSubcategoryName { get; set; } = null;

    public string? Birthday { get; set; }


}