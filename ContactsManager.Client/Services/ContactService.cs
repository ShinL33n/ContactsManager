using ContactsManager.Client.Services.IServices;
using ContactsManager.Shared.DTOs;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace ContactsManager.Client.Services;

public class ContactService : IContactService
{
    private readonly HttpClient _httpClient;
    public ContactService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ContactDetailsDTO>> GetAllContactsDetailsAsync()
    {
         return await _httpClient.GetFromJsonAsync<List<ContactDetailsDTO>>("api/contacts") ?? [];
    }

    public async Task<List<ContactPublicDetailsDTO>> GetAllContactsPublicDetailsAsync()
    {
        throw new NotImplementedException();
    }
}
