using ContactsManager.Shared.DTOs;

namespace ContactsManager.Client.Services.IServices;

public interface IContactService
{
    Task<List<ContactDetailsDTO>> GetAllContactsDetailsAsync();
    Task<List<ContactPublicDetailsDTO>> GetAllContactsPublicDetailsAsync();
    Task<ContactDetailsDTO?> GetContactDetailsByIdAsync(Guid id);
}
