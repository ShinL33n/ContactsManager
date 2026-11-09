using ContactsManager.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;
using System.Text.Json;

namespace ContactsManager.Client.Auth;

public class JwtAuthenticationStateProvider : AuthenticationStateProvider
{
    private static readonly AuthenticationState Anonymous =
        new(new ClaimsPrincipal(new ClaimsIdentity()));

    private readonly IJSRuntime _js;

    public JwtAuthenticationStateProvider(IJSRuntime js) => _js = js;

    /// <summary>
    /// Builds the current user from the stored token (or anonymous if there is none).
    /// </summary>
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _js.InvokeAsync<string?>("localStorage.getItem", AuthService.TokenKey);
        if (string.IsNullOrEmpty(token))
            return Anonymous;

        var values = ReadPayload(token);

        // "exp" is a Unix timestamp in seconds; an expired token means not logged in
        if (values.TryGetValue("exp", out var exp) &&
            DateTimeOffset.FromUnixTimeSeconds(exp.GetInt64()) < DateTimeOffset.UtcNow)
            return Anonymous;

        var claims = values.Select(kv => new Claim(kv.Key, kv.Value.ToString()));
        var identity = new ClaimsIdentity(claims, "jwt", ClaimTypes.Email, ClaimTypes.Role);
        return new AuthenticationState(new ClaimsPrincipal(identity));
    }

    /// <summary>
    /// Tells Blazor that the login state changed so the UI re-renders.
    /// </summary>
    public void NotifyStateChanged()
        => NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

    // The payload is the middle part of the token, Base64Url encoded JSON
    private static Dictionary<string, JsonElement> ReadPayload(string jwt)
    {
        var payload = jwt.Split('.')[1].Replace('-', '+').Replace('_', '/');
        payload = payload.PadRight(payload.Length + (4 - payload.Length % 4) % 4, '=');
        return JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(Convert.FromBase64String(payload))!;
    }
}

