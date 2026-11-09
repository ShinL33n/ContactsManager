using ContactsManager.Api.Mappings;
using ContactsManager.Api.Models;
using ContactsManager.Api.Services.IServices;
using ContactsManager.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContactsManager.Api.Controllers;

[ApiController] [Route("api/[controller]")] //[Authorize]
public class ContactsController : Controller
{
    private readonly IUserService _userService;

    public ContactsController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet] [AllowAnonymous]
    public async Task<ActionResult<List<ContactPublicDTO>>> GetAllPublicAsync()
    {
        IEnumerable<User> users = await _userService.GetAllUsersAsync();

        List<ContactPublicDTO> contacts = new();
        // if(not anonymus) return ContactPublicDetailsDto
        foreach (var user in users)
        {
            contacts.Add(user.ToContactPublicDTO());
            //contacts.Add(new ContactPublicDTO
            //{
            //    Id = user.Id,
            //    FirstName = user.FirstName,
            //    LastName = user.LastName,
            //    Email = user.Email,
            //    PhoneNumber = user.PhoneNumber,
            //    CategoryName = user.Category.Name,
            //    SubcategoryName = user.Subcategory?.Name,
            //    CustomSubcategoryName = user.CustomSubcategory?.Name,
            //    Age = user.Age
            //});
        }

        return Ok(contacts);
    }

    [HttpGet("full")]
    public async Task<ActionResult<List<ContactDetailsDTO>>> GetAllAsync()
    {
        IEnumerable<User> users = await _userService.GetAllUsersAsync();

        List<ContactDetailsDTO> contacts = new();
        // if(not anonymus) return ContactPublicDetailsDto
        foreach (var user in users)
        {
            contacts.Add(user.ToContactDetailsDTO());
            //contacts.Add(new ContactDetailsDTO
            //{
            //    Id = user.Id,
            //    FirstName = user.FirstName,
            //    LastName = user.LastName,
            //    Email = user.Email,
            //    PhoneNumber = user.PhoneNumber,
            //    Birthday = user.Birthday,
            //    CategoryName = user.Category.Name,
            //    SubcategoryName = user.Subcategory?.Name,
            //    CustomSubcategoryName = user.CustomSubcategory?.Name,
            //    Age = user.Age,
            //    Address = user.Address,
            //    JobTitle = user.JobTitle,
            //    Education = user.Education?.Name,
            //    Notes = user.Notes
            //});
        }

        return Ok(contacts);
    }

    [HttpGet("{id:guid}")] [AllowAnonymous]
    public async Task<ActionResult<ContactPublicDTO>> GetPublicByIdAsync(Guid id)
    {
        User? user = await _userService.GetUserByIdAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        ContactPublicDTO contact = user.ToContactPublicDTO();

        //ContactPublicDTO contact = new()
        //{
        //    Id = user.Id,
        //    FirstName = user.FirstName,
        //    LastName = user.LastName,
        //    Email = user.Email,
        //    PhoneNumber = user.PhoneNumber,
        //    CategoryName = user.Category.Name,
        //    SubcategoryName = user.Subcategory?.Name,
        //    CustomSubcategoryName = user.CustomSubcategory?.Name,
        //    Age = user.Age
        //};

        return Ok(contact);
    }

    [HttpGet("{id:guid}/full")]
    public async Task<ActionResult<ContactDetailsDTO>> GetByIdAsync(Guid id)
    {
        User? user = await _userService.GetUserByIdAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        ContactDetailsDTO contact = user.ToContactDetailsDTO();
        //ContactDetailsDTO contact = new()
        //{
        //    Id = user.Id,
        //    FirstName = user.FirstName,
        //    LastName = user.LastName,
        //    Email = user.Email,
        //    PhoneNumber = user.PhoneNumber,
        //    Birthday = user.Birthday,
        //    CategoryName = user.Category.Name,
        //    SubcategoryName = user.Subcategory?.Name,
        //    CustomSubcategoryName = user.CustomSubcategory?.Name,
        //    Age = user.Age,
        //    Address = user.Address,
        //    JobTitle = user.JobTitle,
        //    Education = user.Education?.Name,
        //    Notes = user.Notes
        //};

        return Ok(contact);
    }


}
