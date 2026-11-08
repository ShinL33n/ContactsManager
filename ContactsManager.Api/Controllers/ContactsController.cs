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
    private readonly ApiResponse _response;

    public ContactsController(IUserService userService)
    {
        _userService = userService;
        _response = new ApiResponse();
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse>> GetAllAsync()
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
        

        _response.Result = contacts;
        _response.StatusCode = HttpStatusCode.OK;
        return Ok(_response);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ApiResponse>> GetByIdAsync(Guid id)
    {
        User? user = await _userService.GetUserByIdAsync(id);

        if (user == null)
        {
            _response.IsSuccess = false;
            _response.StatusCode = HttpStatusCode.NotFound;
            _response.ErrorMessages.Add("User Not Found");
            return NotFound(_response);
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

        _response.Result = contact;
        _response.StatusCode = HttpStatusCode.OK;
        return Ok(_response);
    }


}
