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
                CategoryId = CategoryPrivate,
                SubcategoryId = null,
                CustomSubcategoryId = null,
                Category = null!,
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
                Category = null!,
                Subcategory = null!,
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
                Category = null!,
                Subcategory = null!,
                CustomSubcategory = null!
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
                Category = null!,
                Subcategory = null!,
                CustomSubcategory = null!
            }
        );
    }
}
