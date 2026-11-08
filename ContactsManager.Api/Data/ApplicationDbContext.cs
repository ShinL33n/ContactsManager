using ContactsManager.Api.Models;
using ContactsManager.Shared.Enums;
using ContactsManager.Shared.Utils;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactsManager.Api.Data;

public class ApplicationDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
                                IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Subcategory> Subcategories { get; set; }
    public DbSet<CustomSubcategory> CustomSubcategories { get; set; }
    public DbSet<Education> Educations { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Categories
        var CategoryBusiness = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var CategoryPrivate = Guid.Parse("22222222-2222-2222-2222-222222222222");
        var CategoryOther = Guid.Parse("33333333-3333-3333-3333-333333333333");

        // Subcategories
        var SubcategoryBoss = Guid.Parse("44444444-4444-4444-4444-444444444441");
        var SubcategoryClient = Guid.Parse("44444444-4444-4444-4444-444444444442");
        var SubcategoryCoworker = Guid.Parse("44444444-4444-4444-4444-444444444443");
        var SubcategoryHR = Guid.Parse("44444444-4444-4444-4444-444444444443");

        // CustomSubcategories
        var CustomSubcategoryNeighbor = Guid.Parse("55555555-5555-5555-5555-555555555551");
        var CustomSubcategoryEmployee = Guid.Parse("55555555-5555-5555-5555-555555555552");

        // Education
        var EducationPrimary = Guid.Parse("66666666-6666-6666-6666-666666666661");
        var EducationVocational = Guid.Parse("66666666-6666-6666-6666-666666666662");
        var EducationSecondary = Guid.Parse("66666666-6666-6666-6666-666666666663");
        var EducationBachelor = Guid.Parse("66666666-6666-6666-6666-666666666664");
        var EducationEngineer = Guid.Parse("66666666-6666-6666-6666-666666666665");
        var EducationMaster = Guid.Parse("66666666-6666-6666-6666-666666666666");
        var EducationDoctorate = Guid.Parse("66666666-6666-6666-6666-666666666667");


        builder.Entity<Subcategory>().HasData(
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
        );

        builder.Entity<Category>().HasData(
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
        );

        builder.Entity<CustomSubcategory>().HasData(
            new CustomSubcategory
            {
                Name = "sąsiad",
            },
            new CustomSubcategory
            {
                Name = "przysły pracownik"
            }
        );

        builder.Entity<Education>().HasData(
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
        );

        builder.Entity<User>().HasData(
            new User
            {
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
                Category = null!,
                Subcategory = null!,
                CustomSubcategory = null!,
                Education = null!
            },
            new User
            {
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
                Category = null!,
                Subcategory = null!,
                CustomSubcategory = null!,
                Education = null!
            },
            new User
            {
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
                Category = null!,
                Subcategory = null!,
                CustomSubcategory = null!,
                Education = null!
            },
            new User
            {
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
                Category = null!,
                Subcategory = null!,
                CustomSubcategory = null!,
                Education = null!
            }
        );
    }
}
