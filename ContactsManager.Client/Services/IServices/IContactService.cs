using ContactsManager.Shared.DTOs;

namespace ContactsManager.Client.Services.IServices;

public interface IContactService
{
    Task<IReadOnlyList<ContactPublicDTO>> GetAllContactsAsync();
    Task<ContactPublicDTO?> GetContactDetailsByIdAsync(Guid id);
    Task<ContactDetailsDTO?> GetEditableContactAsync(Guid id);
    Task<ContactDetailsDTO> CreateContactAsync(ContactCreateDTO contact);
    Task<ContactDetailsDTO> UpdateContactAsync(ContactUpdateDTO contact);
    Task DeleteContactAsync(Guid id);
}
