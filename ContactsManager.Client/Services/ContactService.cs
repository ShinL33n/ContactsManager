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

    public async Task<ContactDetailsDTO?> GetEditableContactAsync(Guid id)
    {
        return await _httpClient.GetFromJsonAsync<ContactDetailsDTO>($"api/contacts/{id}/full");
    }

    public async Task<ContactDetailsDTO> CreateContactAsync(ContactCreateDTO contact)
    {
        using var response = await _httpClient.PostAsJsonAsync("api/contacts", contact);
        await EnsureSuccessAsync(response);
        return await response.Content.ReadFromJsonAsync<ContactDetailsDTO>()
            ?? throw new InvalidOperationException("The API returned no created contact.");
    }

    public async Task<ContactDetailsDTO> UpdateContactAsync(ContactUpdateDTO contact)
    {
        using var response = await _httpClient.PutAsJsonAsync("api/contacts", contact);
        await EnsureSuccessAsync(response);
        return await response.Content.ReadFromJsonAsync<ContactDetailsDTO>()
            ?? throw new InvalidOperationException("The API returned no updated contact.");
    }

    public async Task DeleteContactAsync(Guid id)
    {
        using var response = await _httpClient.DeleteAsync($"api/contacts/{id}/delete");
        response.EnsureSuccessStatusCode();
    }

    private static async Task EnsureSuccessAsync(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            return;
        }

        var message = await response.Content.ReadAsStringAsync();
        throw new HttpRequestException(string.IsNullOrWhiteSpace(message)
            ? $"API returned status {(int)response.StatusCode}."
            : message);
    }

    private async Task<bool> IsLoggedInAsync()
    {
        var state = await _authState.GetAuthenticationStateAsync();
        return state.User.Identity?.IsAuthenticated == true;
    }
}
