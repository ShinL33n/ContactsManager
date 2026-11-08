using ContactsManager.Api.Models;
using ContactsManager.Api.Services.IServices;
using ContactsManager.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ContactsManager.Api.Controllers;

[ApiController]
//[Authorize]
[Route("api/[controller]")]
public class ContactsController : Controller
{
    private readonly IUserService _userService;

    public ContactsController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<List<ContactDetailsDTO>>> GetAllAsync()
    {
        IEnumerable<User> users = await _userService.GetAllUsersAsync();

        List<ContactDetailsDTO> contacts = new();
        // if(not anonymus) return ContactPublicDetailsDto
        foreach (var user in users)
        {
            contacts.Add(new ContactDetailsDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Birthday = user.Birthday?.ToString("dd.MM.yyyy"),
                CategoryName = user.Category.Name,
                SubcategoryName = user.Subcategory?.Name,
                CustomSubcategoryName = user.CustomSubcategory?.Name
            });
        }

        return Ok(contacts);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ContactDetailsDTO>> GetByIdAsync(Guid id)
    {
        User? user = await _userService.GetUserByIdAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        ContactDetailsDTO contact = new()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Birthday = user.Birthday?.ToString("dd.MM.yyyy"),
            CategoryName = user.Category.Name,
            SubcategoryName = user.Subcategory?.Name,
            CustomSubcategoryName = user.CustomSubcategory?.Name
        };

        return Ok(contact);
    }


}
