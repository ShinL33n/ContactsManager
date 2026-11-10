using ContactsManager.Shared.DTOs;
using ContactsManager.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace ContactsManager.Client.Models;

public sealed class ContactFormModel : IValidatableObject
{
    public Guid Id { get; set; }
    public bool IsEditMode { get; set; }
    public SubcategoryMode SelectedSubcategoryMode { get; set; } = SubcategoryMode.None;

    [Required(ErrorMessage = "Imię jest wymagane.")]
    public string FirstName { get; set; } = "";

    [Required(ErrorMessage = "Nazwisko jest wymagane.")]
    public string LastName { get; set; } = "";

    [Required(ErrorMessage = "Email jest wymagany.")]
    [EmailAddress(ErrorMessage = "Podaj prawidłowy adres email.")]
    public string Email { get; set; } = "";

    public string? PhoneNumber { get; set; }
    public DateOnly? Birthday { get; set; }
    public string? Address { get; set; }
    public string? JobTitle { get; set; }
    public string? Notes { get; set; }

    [Required(ErrorMessage = "Wybierz kategorię.")]
    public Guid CategoryId { get; set; }

    public Guid? SubcategoryId { get; set; }
    public Guid? CustomSubcategoryId { get; set; }
    public string? CustomSubcategoryName { get; set; }
    public Guid? EducationId { get; set; }

    public string? Password { get; set; }
    public string? ConfirmPassword { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!IsEditMode && string.IsNullOrWhiteSpace(Password))
        {
            yield return new ValidationResult("Hasło jest wymagane.", [nameof(Password)]);
        }

        if (!string.IsNullOrWhiteSpace(Password))
        {
            if (Password.Length is < 8 or > 100 ||
                !Password.Any(char.IsLower) ||
                !Password.Any(char.IsUpper) ||
                !Password.Any(char.IsDigit) ||
                Password.All(char.IsLetterOrDigit))
            {
                yield return new ValidationResult(
                    "Hasło musi mieć co najmniej 8 znaków oraz zawierać małą i wielką literę, cyfrę i znak specjalny.",
                    [nameof(Password)]);
            }

            if (!string.Equals(Password, ConfirmPassword, StringComparison.Ordinal))
            {
                yield return new ValidationResult("Hasła nie są zgodne.", [nameof(ConfirmPassword)]);
            }
        }

        if (CategoryId == Guid.Empty)
        {
            yield return new ValidationResult("Wybierz kategorię.", [nameof(CategoryId)]);
        }
        else if (SelectedSubcategoryMode == SubcategoryMode.Dictionary && !SubcategoryId.HasValue)
        {
            yield return new ValidationResult("Wybierz podkategorię.", [nameof(SubcategoryId)]);
        }
        else if (SelectedSubcategoryMode == SubcategoryMode.Custom && string.IsNullOrWhiteSpace(CustomSubcategoryName))
        {
            yield return new ValidationResult("Wpisz własną podkategorię.", [nameof(CustomSubcategoryName)]);
        }
    }

    public ContactCreateDTO ToCreateDto() => new()
    {
        FirstName = FirstName,
        LastName = LastName,
        Email = Email,
        PhoneNumber = PhoneNumber,
        Birthday = Birthday,
        Address = Address,
        JobTitle = JobTitle,
        Notes = Notes,
        CategoryId = CategoryId,
        SubcategoryId = SubcategoryId,
        CustomSubcategoryId = CustomSubcategoryId,
        CustomSubcategoryName = CustomSubcategoryName,
        EducationId = EducationId,
        Password = Password ?? "",
        ConfirmPassword = ConfirmPassword ?? ""
    };

    public ContactUpdateDTO ToUpdateDto() => new()
    {
        Id = Id,
        FirstName = FirstName,
        LastName = LastName,
        Email = Email,
        PhoneNumber = PhoneNumber,
        Birthday = Birthday,
        Address = Address,
        JobTitle = JobTitle,
        Notes = Notes,
        CategoryId = CategoryId,
        SubcategoryId = SubcategoryId,
        CustomSubcategoryId = CustomSubcategoryId,
        CustomSubcategoryName = CustomSubcategoryName,
        EducationId = EducationId,
        Password = Password,
        ConfirmPassword = ConfirmPassword
    };

    public static ContactFormModel FromContact(ContactDetailsDTO contact) => new()
    {
        Id = contact.Id,
        IsEditMode = true,
        FirstName = contact.FirstName,
        LastName = contact.LastName,
        Email = contact.Email,
        PhoneNumber = contact.PhoneNumber,
        Birthday = contact.Birthday,
        Address = contact.Address,
        JobTitle = contact.JobTitle,
        Notes = contact.Notes,
        CategoryId = contact.CategoryId,
        SubcategoryId = contact.SubcategoryId,
        CustomSubcategoryId = contact.CustomSubcategoryId,
        CustomSubcategoryName = contact.CustomSubcategoryName,
        EducationId = contact.EducationId
    };
}
