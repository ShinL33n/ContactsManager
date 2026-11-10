using ContactsManager.Shared.DTOs;

namespace ContactsManager.Client.Services.IServices;

public interface IAuthService
{
    string? LastError { get; }
    Task<bool> LoginAsync(LoginRequestDTO model);
    Task<bool> RegisterAsync(RegisterRequestDTO model);
    Task LogoutAsync();
}
