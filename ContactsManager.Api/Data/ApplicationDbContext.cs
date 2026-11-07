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

        builder.Entity<Subcategory>().HasData(
            new Subcategory
            {
                Name = SubcategoryNames.Boss
            },
            new Subcategory
            {
                Name = SubcategoryNames.Client
            },
            new Subcategory
            {
                Name = SubcategoryNames.Coworker
            },
            new Subcategory
            {
                Name = SubcategoryNames.HR
            }
        );

        builder.Entity<Category>().HasData(
            new Category
            {
                Name = CategoryNames.Private,
                SubcategoryMode = SubcategoryMode.None
            },
            new Category
            {
                Name = CategoryNames.Business,
                SubcategoryMode = SubcategoryMode.Dictionary
            },
            new Category
            {
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
        );
    }
}
