using ContactsManager.Client.Services.IServices;
using ContactsManager.Shared.DTOs;
using System.Net.Http.Json;

namespace ContactsManager.Client.Services;

public class ContactService : IContactService
{
    private readonly HttpClient _httpClient;
    public ContactService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IReadOnlyList<ContactPublicDTO>> GetAllContactsAsync()
    {
        if(await IsLoggedInAsync())
        {
            var fullDetailsContacts = await _httpClient.GetFromJsonAsync<List<ContactDetailsDTO>>("api/contacts/full") ?? [];
            return fullDetailsContacts;
        }

        return await _httpClient.GetFromJsonAsync<List<ContactPublicDTO>>("api/contacts") ?? [];
    }

    public async Task<ContactPublicDTO?> GetContactDetailsByIdAsync(Guid id)
    {
        if (await IsLoggedInAsync())
        {
            var fullDetailsContact = await _httpClient.GetFromJsonAsync<ContactDetailsDTO>($"api/contacts/{id}/full") ?? null;
            return fullDetailsContact;
        }

        return await _httpClient.GetFromJsonAsync<ContactPublicDTO>($"api/contacts/{id}") ?? null;
    }

    private async Task<bool> IsLoggedInAsync()
    {
        return await Task.FromResult(true);
        // auth function here
    }
}
