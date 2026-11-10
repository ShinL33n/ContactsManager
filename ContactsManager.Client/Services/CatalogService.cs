using ContactsManager.Client.Services.IServices;
using ContactsManager.Shared.DTOs;
using System.Net.Http.Json;

namespace ContactsManager.Client.Services;

public sealed class CatalogService : ICatalogService
{
    private readonly HttpClient _httpClient;

    public CatalogService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ContactFormOptionsDTO> GetContactFormOptionsAsync()
    {
        return await _httpClient.GetFromJsonAsync<ContactFormOptionsDTO>("api/catalog/contact-form")
            ?? throw new InvalidOperationException("The API returned no contact form options.");
    }
}
