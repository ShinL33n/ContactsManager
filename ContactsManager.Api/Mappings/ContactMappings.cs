using ContactsManager.Api.Models;
using ContactsManager.Shared.DTOs;

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

            // tmp solution, just to compile
            Category = new Category { Id = contactCreateDTO.CategoryId },
        };
    }

    public static User ToUser(this ContactUpdateDTO contactUpdateDTO)
    {
        return new User
        {
            // Id stays the same
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

            // tmp solution, just to compile
            Category = new Category { Id = contactUpdateDTO.CategoryId },
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

            // tmp solution, just to compile
            CategoryId = Guid.Parse("22222222-2222-2222-2222-222222222222"), // default category - private
            Category = new Category { Id = Guid.Parse("22222222-2222-2222-2222-222222222222") },
        };
    }
}
