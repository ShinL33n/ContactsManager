using ContactsManager.Api.Models;
using ContactsManager.Api.Services.IServices;
using ContactsManager.Shared.Enums;
using ContactsManager.Shared.Utils;
using System.Data;

namespace ContactsManager.Api.Services;

public class MockUserService : IUserService
{
    // Categories
    private static readonly Guid CategoryBusiness = Guid.Parse("11111111-1111-1111-1111-111111111111");
    private static readonly Guid CategoryPrivate = Guid.Parse("22222222-2222-2222-2222-222222222222");
    private static readonly Guid CategoryOther = Guid.Parse("33333333-3333-3333-3333-333333333333");

    // Subcategories
    private static readonly Guid SubcategoryBoss = Guid.Parse("44444444-4444-4444-4444-444444444441");
    private static readonly Guid SubcategoryClient = Guid.Parse("44444444-4444-4444-4444-444444444442");
    private static readonly Guid SubcategoryCoworker = Guid.Parse("44444444-4444-4444-4444-444444444443");
    private static readonly Guid SubcategoryHR = Guid.Parse("44444444-4444-4444-4444-444444444443");

    // CustomSubcategories
    private static readonly Guid CustomSubcategoryNeighbor = Guid.Parse("55555555-5555-5555-5555-555555555551");
    private static readonly Guid CustomSubcategoryEmployee = Guid.Parse("55555555-5555-5555-5555-555555555552");


    private readonly List<User> users;
    private readonly List<Category> categories;
    private readonly List<Subcategory> subcategories;
    private readonly List<CustomSubcategory> customsubcategories;

    public MockUserService()
    {
        subcategories = new()
        {
            new Subcategory
            {
                Id = SubcategoryBoss,
                Name = SubcategoryNames.Boss
            },
            new Subcategory
            {
                Id = SubcategoryClient,
                Name = SubcategoryNames.Client
            },
            new Subcategory
            {
                Id = SubcategoryCoworker,
                Name = SubcategoryNames.Coworker
            },
            new Subcategory
            {
                Id = SubcategoryHR,
                Name = SubcategoryNames.HR
            }
        };

        categories = new()
        {
            new Category
            {
                Id = CategoryPrivate,
                Name = CategoryNames.Private,
                SubcategoryMode = SubcategoryMode.None
            },
            new Category
            {
                Id = CategoryBusiness,
                Name = CategoryNames.Business,
                SubcategoryMode = SubcategoryMode.Dictionary
            },
            new Category
            {
                Id = CategoryOther,
                Name = CategoryNames.Other,
                SubcategoryMode = SubcategoryMode.Custom
            }
        };

        customsubcategories = new()
        {
            new CustomSubcategory
            {
                Id = CustomSubcategoryNeighbor,
                Name = "sąsiad",
            },
            new CustomSubcategory
            {
                Id = CustomSubcategoryEmployee,
                Name = "przyszły pracownik"
            }
        };

        users = new()
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
                CategoryId = CategoryPrivate,
                SubcategoryId = null,
                CustomSubcategoryId = null,
                Category = categories.FirstOrDefault(c => c.Id == CategoryPrivate)!,
                Subcategory = null!,
                CustomSubcategory = null!
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
                CategoryId = CategoryBusiness,
                SubcategoryId = SubcategoryCoworker,
                CustomSubcategoryId = null,
                Category = categories.FirstOrDefault(c => c.Id == CategoryBusiness)!,
                Subcategory = subcategories.FirstOrDefault(s => s.Id == SubcategoryCoworker)!,
                CustomSubcategory = null!
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
                CategoryId = CategoryOther,
                SubcategoryId = null,
                CustomSubcategoryId = CustomSubcategoryNeighbor,
                Category = categories.FirstOrDefault(c => c.Id == CategoryOther)!,
                Subcategory = null!,
                CustomSubcategory = customsubcategories.FirstOrDefault(c => c.Id == CustomSubcategoryNeighbor)!
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
                CategoryId = CategoryOther,
                SubcategoryId = null,
                CustomSubcategoryId = CustomSubcategoryEmployee,
                Category = categories.FirstOrDefault(c => c.Id == CategoryOther)!,
                Subcategory = null!,
                CustomSubcategory = customsubcategories.FirstOrDefault(c => c.Id == CustomSubcategoryEmployee)!
            }
        };
    }

    public async Task<bool> CreateUserAsync(User user)
    {
        if (user != null)
        {
            user.Id = Guid.NewGuid();
            users.Add(user);
            return await Task.FromResult(true);
        }
        return await Task.FromResult(false);
    }

    public async Task<bool> DeleteUserAsync(Guid id)
    {
        User? user = users.FirstOrDefault(u => u.Id == id);
        if (user != null)
        {
            users.Remove(user);
            return await Task.FromResult(true);
        }
        return await Task.FromResult(false);
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await Task.FromResult(users.AsEnumerable());
    }

    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        User? user = users.FirstOrDefault(u => u.Id == id);
        return await Task.FromResult(user);
    }

    public async Task<bool> UpdateUserAsync(User user)
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
            updatedUser.CategoryId = user.CategoryId;
            updatedUser.SubcategoryId = user.SubcategoryId;
            updatedUser.CustomSubcategoryId = user.CustomSubcategoryId;
        }
        return await Task.FromResult(false);
    }
}
