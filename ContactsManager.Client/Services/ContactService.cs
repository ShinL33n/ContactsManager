using ContactsManager.Client.Auth;
using ContactsManager.Client.Services.IServices;
using ContactsManager.Shared.DTOs;
using System.Net.Http.Json;

namespace ContactsManager.Client.Services;

public class ContactService : IContactService
{
    private readonly HttpClient _httpClient;
    private readonly JwtAuthenticationStateProvider _authState;
    public ContactService(HttpClient httpClient, JwtAuthenticationStateProvider authState)
    {
        _httpClient = httpClient;
        _authState = authState;
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
        var state = await _authState.GetAuthenticationStateAsync();
        return state.User.Identity?.IsAuthenticated == true;
    }
}
