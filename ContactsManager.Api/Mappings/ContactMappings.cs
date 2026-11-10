using ContactsManager.Api.Models;
using ContactsManager.Shared.DTOs;
using ContactsManager.Shared.Utils;

namespace ContactsManager.Api.Mappings;

public static class ContactMappings
{
    public static User ToUser(this ContactCreateDTO contactCreateDTO)
    {
        return new User
        {
            // Id sets automatically in the User constructor
            FirstName = contactCreateDTO.FirstName,
            LastName = contactCreateDTO.LastName,
            Email = contactCreateDTO.Email,
            PhoneNumber = contactCreateDTO.PhoneNumber,
            Birthday = contactCreateDTO.Birthday,
            Age = contactCreateDTO.Age,
            Address = contactCreateDTO.Address,
            JobTitle = contactCreateDTO.JobTitle,
            Notes = contactCreateDTO.Notes,
            CategoryId = contactCreateDTO.CategoryId,
            SubcategoryId = contactCreateDTO.SubcategoryId,
            CustomSubcategoryId = contactCreateDTO.CustomSubcategoryId,
            EducationId = contactCreateDTO.EducationId,
            Role = RoleNames.UserRole,

            // The service validates and loads the category by its foreign key.
            Category = null!,
        };
    }

    public static User ToUser(this ContactUpdateDTO contactUpdateDTO)
    {
        return new User
        {
            Id = contactUpdateDTO.Id,
            FirstName = contactUpdateDTO.FirstName,
            LastName = contactUpdateDTO.LastName,
            Email = contactUpdateDTO.Email,
            PhoneNumber = contactUpdateDTO.PhoneNumber,
            Birthday = contactUpdateDTO.Birthday,
            Age = contactUpdateDTO.Age,
            Address = contactUpdateDTO.Address,
            JobTitle = contactUpdateDTO.JobTitle,
            Notes = contactUpdateDTO.Notes,
            CategoryId = contactUpdateDTO.CategoryId,
            SubcategoryId = contactUpdateDTO.SubcategoryId,
            CustomSubcategoryId = contactUpdateDTO.CustomSubcategoryId,
            EducationId = contactUpdateDTO.EducationId,

            // The service resolves the real category and related dictionary entities.
            Category = null!,
        };
    }

    public static User ToUser(this RegisterRequestDTO registerRequestDTO)
    {
        return new User
        {
            // Id sets automatically in the User constructor
            FirstName = registerRequestDTO.FirstName,
            LastName = registerRequestDTO.LastName,
            Email = registerRequestDTO.Email,

            // The service assigns the database category marked as private.
            Category = null!,
        };
    }
}
