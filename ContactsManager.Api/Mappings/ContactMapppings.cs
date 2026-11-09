using ContactsManager.Api.Models;
using ContactsManager.Shared.DTOs;

namespace ContactsManager.Api.Mappings;

/// <summary>
/// Provides extension methods for mapping user entities to contact DTOs.
/// </summary>
public static class ContactMapppings
{
    public static ContactPublicDTO ToContactPublicDTO(this User user)
    {
        return new ContactPublicDTO
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            CategoryName = user.Category.Name,
            SubcategoryName = user.Subcategory?.Name,
            CustomSubcategoryName = user.CustomSubcategory?.Name,
            Age = user.Age
        };
    }

    public static ContactDetailsDTO ToContactDetailsDTO(this User user)
    {
        return new ContactDetailsDTO
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            CategoryName = user.Category.Name,
            SubcategoryName = user.Subcategory?.Name,
            CustomSubcategoryName = user.CustomSubcategory?.Name,
            Age = user.Age,

            Birthday = user.Birthday,
            Address = user.Address,
            JobTitle = user.JobTitle,
            Education = user.Education?.Name,
            Notes = user.Notes
        };
    }
}
