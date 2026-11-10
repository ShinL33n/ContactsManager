using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ContactsManager.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubcategoryMode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomSubcategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomSubcategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Educations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Educations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subcategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subcategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Birthday = table.Column<DateOnly>(type: "date", nullable: true),
                    Age = table.Column<long>(type: "bigint", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubcategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CustomSubcategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EducationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_CustomSubcategories_CustomSubcategoryId",
                        column: x => x.CustomSubcategoryId,
                        principalTable: "CustomSubcategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Educations_EducationId",
                        column: x => x.EducationId,
                        principalTable: "Educations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Subcategories_SubcategoryId",
                        column: x => x.SubcategoryId,
                        principalTable: "Subcategories",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "SubcategoryMode" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Służbowy", 1 },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Prywatny", 0 },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "Inny", 2 }
                });

            migrationBuilder.InsertData(
                table: "CustomSubcategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("55555555-5555-5555-5555-555555555551"), "sąsiad" },
                    { new Guid("55555555-5555-5555-5555-555555555552"), "przyszły pracownik" }
                });

            migrationBuilder.InsertData(
                table: "Educations",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("66666666-6666-6666-6666-666666666661"), "Podstawowe" },
                    { new Guid("66666666-6666-6666-6666-666666666662"), "Zawodowe" },
                    { new Guid("66666666-6666-6666-6666-666666666663"), "Średnie" },
                    { new Guid("66666666-6666-6666-6666-666666666664"), "Licencjat" },
                    { new Guid("66666666-6666-6666-6666-666666666665"), "Inżynier" },
                    { new Guid("66666666-6666-6666-6666-666666666666"), "Magister" },
                    { new Guid("66666666-6666-6666-6666-666666666667"), "Doktor" }
                });

            migrationBuilder.InsertData(
                table: "Subcategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("44444444-4444-4444-4444-444444444441"), "Szef" },
                    { new Guid("44444444-4444-4444-4444-444444444442"), "Klient" },
                    { new Guid("44444444-4444-4444-4444-444444444443"), "Współpracownik" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "HR" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "Age", "Birthday", "CategoryId", "CustomSubcategoryId", "EducationId", "Email", "FirstName", "JobTitle", "LastName", "Notes", "PasswordHash", "PhoneNumber", "Role", "SubcategoryId" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "ul. Przykładowa 1, 00-001 Warszawa", 36L, new DateOnly(1990, 1, 12), new Guid("22222222-2222-2222-2222-222222222222"), null, new Guid("66666666-6666-6666-6666-666666666665"), "adam@example.com", "Adam", "Inżynier oprogramowania", "Kowalski", "Software Engineer with 10 years of experience.", "AQAAAAIAAYagAAAAEGzAAReeGaf3kMwmtIasd/lp0Ihh1p+k0kg02G7tr/388Kpg6MEU3/KbuWVAurBSEw==", "123456789", "Użytkownik", null },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "ul. Wymyślona 3, 12-345 Wrocław", 27L, new DateOnly(1999, 2, 10), new Guid("33333333-3333-3333-3333-333333333333"), new Guid("55555555-5555-5555-5555-555555555551"), new Guid("66666666-6666-6666-6666-666666666662"), "jerzy@example.com", "Jerzy", "Sprzedawca", "Lewandowski", "Pracuje w lokalnym sklepie.", "AQAAAAIAAYagAAAAEGzAAReeGaf3kMwmtIasd/lp0Ihh1p+k0kg02G7tr/388Kpg6MEU3/KbuWVAurBSEw==", "324934123", "Użytkownik", null },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "ul. Nieistotna 7, 12-345 Kraków", 30L, new DateOnly(1996, 5, 24), new Guid("11111111-1111-1111-1111-111111111111"), null, new Guid("66666666-6666-6666-6666-666666666666"), "alicja@example.com", "Alicja", "Testerka oprogramowania", "Adamczyk", "Współpracowniczka z innego działu.", "AQAAAAIAAYagAAAAEGzAAReeGaf3kMwmtIasd/lp0Ihh1p+k0kg02G7tr/388Kpg6MEU3/KbuWVAurBSEw==", "890123657", "Użytkownik", new Guid("44444444-4444-4444-4444-444444444443") },
                    { new Guid("00000000-0000-0000-0000-000000000004"), "ul. Wymyślona 4, 00-123 Gdańsk", 24L, new DateOnly(2002, 10, 6), new Guid("33333333-3333-3333-3333-333333333333"), new Guid("55555555-5555-5555-5555-555555555552"), new Guid("66666666-6666-6666-6666-666666666665"), "adrian@example.com", "Adrian", "Student", "Kisieliński", "Zamiłowanie do kodu i technologii", "AQAAAAIAAYagAAAAEPKThCcAvFAmMa+2i4fFyIJBH+5U3k2mShq4CE+4Etod65qjs7g5Tod1YKCqx4Pg2g==", "123123123", "Admin", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomSubcategories_Name",
                table: "CustomSubcategories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CategoryId",
                table: "Users",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CustomSubcategoryId",
                table: "Users",
                column: "CustomSubcategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_EducationId",
                table: "Users",
                column: "EducationId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_SubcategoryId",
                table: "Users",
                column: "SubcategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "CustomSubcategories");

            migrationBuilder.DropTable(
                name: "Educations");

            migrationBuilder.DropTable(
                name: "Subcategories");
        }
    }
}
