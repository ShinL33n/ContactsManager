using ContactsManager.Api.Data;
using ContactsManager.Api.Models;
using ContactsManager.Api.Services.IServices;
using ContactsManager.Shared.Enums;
using ContactsManager.Shared.Utils;
using Microsoft.EntityFrameworkCore;

namespace ContactsManager.Api.Services;

public sealed class UserService : IUserService
{
    private readonly ApplicationDbContext _dbContext;

    public UserService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private IQueryable<User> QueryUsers()
    {
        return _dbContext.Users
            .Include(user => user.Category)
            .Include(user => user.Subcategory)
            .Include(user => user.CustomSubcategory)
            .Include(user => user.Education)
            .AsNoTracking();
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await QueryUsers().ToListAsync();
    }

    public Task<User?> GetUserByIdAsync(Guid id)
    {
        return QueryUsers().FirstOrDefaultAsync(user => user.Id == id);
    }

    public Task<User?> GetUserByEmailAsync(string email)
    {
        return _dbContext.Users.FirstOrDefaultAsync(user => user.Email == email);
    }

    public async Task<bool> CreateUserAsync(User user, string? customSubcategoryName = null)
    {
        if (user.CategoryId == Guid.Empty)
        {
            var privateCategory = await _dbContext.Categories
                .FirstOrDefaultAsync(category => category.Name == CategoryNames.Private);

            if (privateCategory == null)
            {
                return false;
            }

            user.CategoryId = privateCategory.Id;
        }

        if (await _dbContext.Users.AnyAsync(existing => existing.Email == user.Email))
        {
            return false;
        }

        if (!await ReferencesAreValidAsync(user, customSubcategoryName))
        {
            return false;
        }

        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateUserAsync(User user, string? customSubcategoryName = null)
    {
        var existing = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
        if (existing == null || await _dbContext.Users.AnyAsync(u => u.Id != user.Id && u.Email == user.Email))
        {
            return false;
        }

        if (!await ReferencesAreValidAsync(user, customSubcategoryName))
        {
            return false;
        }

        existing.FirstName = user.FirstName;
        existing.LastName = user.LastName;
        existing.Email = user.Email;
        existing.PhoneNumber = user.PhoneNumber;
        existing.Birthday = user.Birthday;
        existing.Age = user.Age;
        existing.Address = user.Address;
        existing.JobTitle = user.JobTitle;
        existing.Notes = user.Notes;
        existing.CategoryId = user.CategoryId;
        existing.SubcategoryId = user.SubcategoryId;
        existing.CustomSubcategoryId = user.CustomSubcategoryId;
        existing.EducationId = user.EducationId;

        if (!string.IsNullOrWhiteSpace(user.PasswordHash))
        {
            existing.PasswordHash = user.PasswordHash;
        }

        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteUserAsync(Guid id)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null)
        {
            return false;
        }

        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    private async Task<bool> ReferencesAreValidAsync(User user, string? customSubcategoryName)
    {
        var category = await _dbContext.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == user.CategoryId);

        if (category == null)
        {
            return false;
        }

        if (category.SubcategoryMode == SubcategoryMode.Custom && !string.IsNullOrWhiteSpace(customSubcategoryName))
        {
            var normalizedName = customSubcategoryName.Trim();
            var customSubcategory = await _dbContext.CustomSubcategories
                .FirstOrDefaultAsync(item => item.Name == normalizedName);

            if (customSubcategory == null)
            {
                customSubcategory = new CustomSubcategory { Name = normalizedName };
                _dbContext.CustomSubcategories.Add(customSubcategory);
                await _dbContext.SaveChangesAsync();
            }

            user.CustomSubcategoryId = customSubcategory.Id;
        }

        var hasSubcategory = user.SubcategoryId.HasValue;
        var hasCustomSubcategory = user.CustomSubcategoryId.HasValue;

        if (category.SubcategoryMode == SubcategoryMode.None && (hasSubcategory || hasCustomSubcategory))
        {
            return false;
        }

        if (category.SubcategoryMode == SubcategoryMode.Dictionary &&
            (!hasSubcategory || hasCustomSubcategory || !await _dbContext.Subcategories.AnyAsync(s => s.Id == user.SubcategoryId)))
        {
            return false;
        }

        if (category.SubcategoryMode == SubcategoryMode.Custom &&
            (!hasCustomSubcategory || hasSubcategory || !await _dbContext.CustomSubcategories.AnyAsync(c => c.Id == user.CustomSubcategoryId)))
        {
            return false;
        }

        return !user.EducationId.HasValue || await _dbContext.Educations.AnyAsync(item => item.Id == user.EducationId);
    }
}
