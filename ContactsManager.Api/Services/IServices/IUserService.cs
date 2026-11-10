using ContactsManager.Api.Models;

namespace ContactsManager.Api.Services.IServices;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User?> GetUserByIdAsync(Guid id);
    Task<User?> GetUserByEmailAsync(string email);
    Task<bool> CreateUserAsync(User user, string? customSubcategoryName = null);
    Task<bool> UpdateUserAsync(User user, string? customSubcategoryName = null);
    Task<bool> DeleteUserAsync(Guid id);
}
