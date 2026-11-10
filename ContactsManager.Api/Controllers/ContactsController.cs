using ContactsManager.Api.Mappings;
using ContactsManager.Api.Models;
using ContactsManager.Api.Services.IServices;
using ContactsManager.Shared.DTOs;
using ContactsManager.Shared.Utils;
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

    //[HttpPost("create")]
    [HttpPost]
    [Authorize(Roles = RoleNames.AdminRole)]
    public async Task<ActionResult<ContactDetailsDTO>> CreateAsync(ContactCreateDTO contactCreateDto)
    {
        // TO-DO: Add CustomSubcategory handling
        try
        {
            if (ModelState.IsValid)
            {
                User newUser = contactCreateDto.ToUser();
                bool createdSuccessfully = await _userService.CreateUserAsync(newUser);

                if (!createdSuccessfully)
                {
                    return BadRequest();
                }

                ContactDetailsDTO contactDetailsDto = newUser.ToContactDetailsDTO();

                return CreatedAtAction(nameof(GetByIdAsync), new { id = newUser.Id }, contactDetailsDto);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    //[HttpPut("update")]
    [HttpPut]
    public async Task<ActionResult<ContactDetailsDTO>> UpdateAsync(ContactUpdateDTO contactUpdateDTO)
    {
        // TO-DO: Add CustomSubcategory handling
        try
        {
            if (ModelState.IsValid)
            {
                User updatedUser = contactUpdateDTO.ToUser();
                bool updatedSuccessfully = await _userService.UpdateUserAsync(updatedUser);

                if (!updatedSuccessfully)
                {
                    return BadRequest();
                }

                ContactDetailsDTO contactDetailsDto = updatedUser.ToContactDetailsDTO();

                return Ok(contactDetailsDto);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    [HttpDelete("{id:guid}/delete")]
    [Authorize(Roles = RoleNames.AdminRole)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        try
        {
            if (ModelState.IsValid)
            {
                bool deletedSuccessfully = await _userService.DeleteUserAsync(id);

                if (!deletedSuccessfully)
                {
                    return NotFound();
                }

                return NoContent();
            }
            else
            {
                return BadRequest(ModelState);  
            }
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
}
