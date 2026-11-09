using Azure;
using ContactsManager.Api.Data;
using ContactsManager.Api.Models;
using ContactsManager.Api.Services.IServices;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ContactsManager.Api.Services;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _dbcontext;
    public UserService(ApplicationDbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        IEnumerable<User> usersList = await _dbcontext.Users
            .Include(u => u.Category)
            .Include(u => u.Subcategory)
            .Include(u => u.CustomSubcategory).ToListAsync();

        return usersList;
    }

    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> CreateUserAsync(User user)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateUserAsync(User user)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteUserAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetUserByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }
}
