using ContactsManager.Client.Pages;
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

    public async Task<List<ContactDetailsDTO>> GetAllContactsDetailsAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<List<ContactDetailsDTO>>("api/contacts") ?? [];
        }
        catch (Exception)
        {
            // log error message
            return [];
        }
    }

    public async Task<List<ContactPublicDetailsDTO>> GetAllContactsPublicDetailsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<ContactDetailsDTO?> GetContactDetailsByIdAsync(Guid id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<ContactDetailsDTO>($"api/contacts/{id}");
        }
        catch (Exception)
        {
            // log error message
            return null;
        }
    }
}
