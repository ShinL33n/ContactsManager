using ContactsManager.Client.Services;
using Microsoft.JSInterop;
using System.Net.Http.Headers;

namespace ContactsManager.Client.Auth;

public class AuthTokenHandler : DelegatingHandler
{
    private readonly IJSRuntime _js;

    public AuthTokenHandler(IJSRuntime js)
    {
        _js = js;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await _js.InvokeAsync<string?>("localStorage.getItem", AuthService.TokenKey);

        if (!string.IsNullOrEmpty(token))
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return await base.SendAsync(request, cancellationToken);
    }
}
