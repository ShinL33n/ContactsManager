using ContactsManager.Api.Mappings;
using ContactsManager.Api.Models;
using ContactsManager.Api.Services.IServices;
using ContactsManager.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContactsManager.Api.Controllers;

[ApiController] [Route("api/[controller]")] [Authorize]
public class ContactsController : ControllerBase
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

        return Ok(contact);
    }


}
