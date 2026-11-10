using ContactsManager.Shared.DTOs;

namespace ContactsManager.Client.Services.IServices;

public interface ICatalogService
{
    Task<ContactFormOptionsDTO> GetContactFormOptionsAsync();
}
