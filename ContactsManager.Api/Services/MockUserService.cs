using ContactsManager.Api.Models;
using ContactsManager.Api.Services.IServices;
using ContactsManager.Shared.Enums;
using ContactsManager.Shared.Utils;
using System.Data;

namespace ContactsManager.Api.Services;

public class MockUserService : IUserService
{
    // Users
    private static readonly Guid UserOne = Guid.Parse("00000000-0000-0000-0000-000000000001");
    private static readonly Guid UserTwo = Guid.Parse("00000000-0000-0000-0000-000000000002");
    private static readonly Guid UserThree = Guid.Parse("00000000-0000-0000-0000-000000000003");
    private static readonly Guid UserFour = Guid.Parse("00000000-0000-0000-0000-000000000004");

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

    // Education
    private static readonly Guid EducationPrimary = Guid.Parse("66666666-6666-6666-6666-666666666661");
    private static readonly Guid EducationVocational = Guid.Parse("66666666-6666-6666-6666-666666666662");
    private static readonly Guid EducationSecondary = Guid.Parse("66666666-6666-6666-6666-666666666663");
    private static readonly Guid EducationBachelor = Guid.Parse("66666666-6666-6666-6666-666666666664");
    private static readonly Guid EducationEngineer = Guid.Parse("66666666-6666-6666-6666-666666666665");
    private static readonly Guid EducationMaster = Guid.Parse("66666666-6666-6666-6666-666666666666");
    private static readonly Guid EducationDoctorate = Guid.Parse("66666666-6666-6666-6666-666666666667");


    private readonly List<User> users;
    private readonly List<Category> categories;
    private readonly List<Subcategory> subcategories;
    private readonly List<CustomSubcategory> customsubcategories;
    private readonly List<Education> educations;

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

        educations = new()
        {
            new Education
            {
                Id = EducationPrimary,
                Name = EducationNames.PrimaryEducation
            },
            new Education
            {
                Id = EducationVocational,
                Name = EducationNames.VocationalEducation
            },
            new Education
            {
                Id = EducationSecondary,
                Name = EducationNames.SecondaryEducation
            },
            new Education
            {
                Id = EducationBachelor,
                Name = EducationNames.BachelorEducation
            },
            new Education
            {
                Id = EducationEngineer,
                Name = EducationNames.EngineerEducation
            },
            new Education
            {
                Id = EducationMaster,
                Name = EducationNames.MasterEducation
            },
            new Education
            {
                Id = EducationDoctorate,
                Name = EducationNames.DoctorateEducation
            }
        };

        users = new()
        {
            new User
            {
                Id = UserOne,
                FirstName = "Adam",
                LastName = "Kowalski",
                Email = "adam@example.com",
                PasswordHash = "passwordToHashInFuture-Adam",
                Role = RoleNames.UserRole,
                PhoneNumber = "123456789",
                Birthday = new DateOnly(1990, 1, 12),
                Age = 36,
                Address = "ul. Przykładowa 1, 00-001 Warszawa",
                JobTitle = "Inżynier oprogramowania",
                Notes = "Software Engineer with 10 years of experience.",
                CategoryId = CategoryPrivate,
                SubcategoryId = null,
                CustomSubcategoryId = null,
                EducationId = EducationEngineer,
                Category = categories.FirstOrDefault(c => c.Id == CategoryPrivate)!,
                Subcategory = null!,
                CustomSubcategory = null!,
                Education = educations.FirstOrDefault(e => e.Id == EducationEngineer)!
            },
            new User
            {
                Id = UserTwo,
                FirstName = "Jerzy",
                LastName = "Lewandowski",
                Email = "jerzy@example.com",
                PasswordHash = "passwordToHashInFuture-Jerzy",
                Role = RoleNames.UserRole,
                PhoneNumber = "324934123",
                Birthday = new DateOnly(1999, 2, 10),
                Age = 27,
                Address = "ul. Wymyślona 3, 12-345 Wrocław",
                JobTitle = "Sprzedawca",
                Notes = "Pracuje w lokalnym sklepie.",
                CategoryId = CategoryOther,
                SubcategoryId = null,
                CustomSubcategoryId = CustomSubcategoryNeighbor,
                EducationId = EducationVocational,
                Category = categories.FirstOrDefault(c => c.Id == CategoryOther)!,
                Subcategory = null!,
                CustomSubcategory = customsubcategories.FirstOrDefault(c => c.Id == CustomSubcategoryNeighbor)!,
                Education = educations.FirstOrDefault(e => e.Id == EducationVocational)!
            },
            new User
            {
                Id = UserThree,
                FirstName = "Alicja",
                LastName = "Adamczyk",
                Email = "alicja@example.com",
                PasswordHash = "passwordToHashInFuture-Alicja",
                Role = RoleNames.UserRole,
                PhoneNumber = "890123657",
                Birthday = new DateOnly(1996, 5, 24),
                Age = 30,
                Address = "ul. Nieistotna 7, 12-345 Kraków",
                JobTitle = "Testerka oprogramowania",
                Notes = "Współpracowniczka z innego działu.",
                CategoryId = CategoryBusiness,
                SubcategoryId = SubcategoryCoworker,
                CustomSubcategoryId = null,
                EducationId = EducationMaster,
                Category = categories.FirstOrDefault(c => c.Id == CategoryBusiness)!,
                Subcategory = subcategories.FirstOrDefault(s => s.Id == SubcategoryCoworker)!,
                CustomSubcategory = null!,
                Education = educations.FirstOrDefault(e => e.Id == EducationMaster)!
            },
            new User
            {
                Id = UserFour,
                FirstName = "Adrian",
                LastName = "Kisieliński",
                Email = "adrian@example.com",
                PasswordHash = "passwordToHashInFuture-Adrian",
                Role = RoleNames.AdminRole,
                PhoneNumber = "123123123",
                Birthday = new DateOnly(2002, 10, 6),
                Age = 24,
                Address = "ul. Wymyślona 4, 00-123 Gdańsk",
                JobTitle = "Student",
                Notes = "Zamiłowanie do kodu i technologii",
                CategoryId = CategoryOther,
                SubcategoryId = null,
                CustomSubcategoryId = CustomSubcategoryEmployee,
                EducationId = EducationEngineer,
                Category = categories.FirstOrDefault(c => c.Id == CategoryOther)!,
                Subcategory = null!,
                CustomSubcategory = customsubcategories.FirstOrDefault(c => c.Id == CustomSubcategoryEmployee)!,
                Education = educations.FirstOrDefault(e => e.Id == EducationEngineer)!
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
