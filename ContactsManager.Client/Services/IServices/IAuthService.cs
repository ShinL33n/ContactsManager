using ContactsManager.Shared.DTOs;

namespace ContactsManager.Client.Services.IServices;

public interface IAuthService
{
    Task<bool> LoginAsync(LoginRequestDTO model);
    Task LogoutAsync();
}
