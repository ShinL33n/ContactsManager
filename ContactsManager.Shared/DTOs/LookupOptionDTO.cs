namespace ContactsManager.Shared.DTOs;

/// <summary>
/// A selectable dictionary value exposed to the client.
/// </summary>
public class LookupOptionDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
}
