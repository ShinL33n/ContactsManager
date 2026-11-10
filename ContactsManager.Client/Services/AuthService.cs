using ContactsManager.Client.Auth;
using ContactsManager.Client.Services.IServices;
using ContactsManager.Shared.DTOs;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace ContactsManager.Client.Services;

public class AuthService : IAuthService
{
    public const string TokenKey = "authToken";

    private readonly HttpClient _http;
    private readonly IJSRuntime _js;
    private readonly JwtAuthenticationStateProvider _authState;

    public string? LastError { get; private set; }

    public AuthService(HttpClient http, IJSRuntime js, JwtAuthenticationStateProvider authState)
    {
        _http = http;
        _js = js;
        _authState = authState;
    }

    /// <summary>
    /// Sends credentials to the API and stores the returned token.
    /// </summary>
    /// <returns>
    /// True if the login succeeded.
    /// </returns>
    public async Task<bool> LoginAsync(LoginRequestDTO model)
    {
        var response = await _http.PostAsJsonAsync("api/auth/login", model);
        if (!response.IsSuccessStatusCode)
            return false;

        var result = await response.Content.ReadFromJsonAsync<LoginResponseDTO>();
        await _js.InvokeVoidAsync("localStorage.setItem", TokenKey, result!.Token);
        _authState.NotifyStateChanged();
        return true;
    }

    /// <summary>
    /// Registers a new user account.
    /// </summary>
    /// <param name="model">The registration details.</param>
    /// <returns>true if registration succeeds; otherwise, false.</returns>
    public async Task<bool> RegisterAsync(RegisterRequestDTO model)
    {
        LastError = null;
        var response = await _http.PostAsJsonAsync("api/auth/register", model);
        if (!response.IsSuccessStatusCode)
        {
            LastError = await response.Content.ReadAsStringAsync();
            return false;
        }

        var result = await response.Content.ReadFromJsonAsync<LoginResponseDTO>();
        if (result is null)
        {
            return false;
        }

        await _js.InvokeVoidAsync("localStorage.setItem", TokenKey, result.Token);
        _authState.NotifyStateChanged();
        return true;
    }

    /// <summary>
    /// Removes the stored token.
    /// </summary>
    public async Task LogoutAsync()
    {
        await _js.InvokeVoidAsync("localStorage.removeItem", TokenKey);
        _authState.NotifyStateChanged();
    }
}
