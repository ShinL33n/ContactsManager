using ContactsManager.Api.Models;
using ContactsManager.Api.Services.IServices;
using ContactsManager.Shared.Utils;
using System.Data;

namespace ContactsManager.Api.Services;

public class MockUserService : IUserService
{
    private List<User> users = new()
    {
        new User
        {
            FirstName = "Adam",
            LastName = "Kowalski",
            Email = "adam@example.com",
            PasswordHash = "passwordToHashInFuture-Adam",
            Role = RoleNames.UserRole,
            PhoneNumber = "123456789",
            Birthday = new DateOnly(1990, 1, 12),
            CategoryName = CategoryNames.Private,
            SubcategoryName = null,
            CustomSubcategoryName = null
        },
        new User
        {
            FirstName = "Alicja",
            LastName = "Adamczyk",
            Email = "alicja@example.com",
            PasswordHash = "passwordToHashInFuture-Alicja",
            Role = RoleNames.UserRole,
            PhoneNumber = "324934123",
            Birthday = new DateOnly(1999, 2, 10),
            CategoryName = CategoryNames.Business,
            SubcategoryName = SubcategoryNames.Coworker,
            CustomSubcategoryName = null
        },
        new User
        {
            FirstName = "Jerzy",
            LastName = "Lewandowski",
            Email = "jerzy@example.com",
            PasswordHash = "passwordToHashInFuture-Jerzy",
            Role = RoleNames.UserRole,
            PhoneNumber = "890123657",
            Birthday = new DateOnly(2001, 5, 24),
            CategoryName = CategoryNames.Other,
            SubcategoryName = null,
            CustomSubcategoryName = "sąsiad"
        },
        new User
        {
            FirstName = "Adrian",
            LastName = "Kisieliński",
            Email = "adrian@example.com",
            PasswordHash = "passwordToHashInFuture-Adrian",
            Role = RoleNames.AdminRole,
            PhoneNumber = "123123123",
            Birthday = new DateOnly(2001, 5, 24),
            CategoryName = CategoryNames.Other,
            SubcategoryName = null,
            CustomSubcategoryName = "przysły pracownik"
        }

    };

    public Task<bool> CreateUserAsync(User user)
    {
        if (user != null)
        {
            user.Id = Guid.NewGuid();
            users.Add(user);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    public Task<bool> DeleteUserAsync(Guid id)
    {
        User? user = users.FirstOrDefault(u => u.Id == id);
        if (user != null)
        {
            users.Remove(user);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    public Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return Task.FromResult(users.AsEnumerable());
    }

    public Task<User?> GetUserByIdAsync(Guid id)
    {
        User? user = users.FirstOrDefault(u => u.Id == id);
        return Task.FromResult(user);
    }

    public Task<bool> UpdateUserAsync(User user)
    {
        User? updatedUser = users.FirstOrDefault(u => u.Id == user.Id);
        if (updatedUser != null)
        {
            updatedUser.FirstName = user.FirstName;
            updatedUser.LastName = user.LastName;
            updatedUser.Email = user.Email;
            updatedUser.PasswordHash = user.PasswordHash;
            updatedUser.Role = user.Role;
            updatedUser.PhoneNumber = user.PhoneNumber;
            updatedUser.Birthday = user.Birthday;
            updatedUser.CategoryName = user.CategoryName;
            updatedUser.SubcategoryName = user.SubcategoryName;
            updatedUser.CustomSubcategoryName = user.CustomSubcategoryName;
        }
        return Task.FromResult(false);
    }
}
