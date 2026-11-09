using ContactsManager.Shared.DTOs;

namespace ContactsManager.Client.Services.IServices;

public interface IContactService
{
    Task<IReadOnlyList<ContactPublicDTO>> GetAllContactsAsync();
    Task<ContactPublicDTO?> GetContactDetailsByIdAsync(Guid id);
}
