using ContactsManager.Api.Mappings;
using ContactsManager.Api.Models;
using ContactsManager.Api.Services.IServices;
using ContactsManager.Shared.DTOs;
using ContactsManager.Shared.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ContactsManager.Api.Controllers;

[ApiController] [Route("api/[controller]")] [Authorize]
public class ContactsController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IPasswordHasher<User> _passwordHasher;

    public ContactsController(IUserService userService, IPasswordHasher<User> passwordHasher)
    {
        _userService = userService;
        _passwordHasher = passwordHasher;
    }

    [HttpGet] [AllowAnonymous]
    public async Task<ActionResult<List<ContactPublicDTO>>> GetAllPublicAsync()
    {
        IEnumerable<User> users = await _userService.GetAllUsersAsync();

        List<ContactPublicDTO> contacts = new();

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


    [HttpPost]
    [Authorize(Roles = RoleNames.AdminRole)]
    public async Task<ActionResult<ContactDetailsDTO>> CreateAsync(ContactCreateDTO contactCreateDto)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        User newUser = contactCreateDto.ToUser();
        newUser.PasswordHash = _passwordHasher.HashPassword(newUser, contactCreateDto.Password);
        bool createdSuccessfully = await _userService.CreateUserAsync(newUser, contactCreateDto.CustomSubcategoryName);

        if (!createdSuccessfully)
        {
            return BadRequest("Email nie jest dostępny lub wybrana podkategoria jest nieprawidłowa.");
        }

        var savedUser = await _userService.GetUserByIdAsync(newUser.Id);
        if (savedUser == null)
        {
            return Problem("Kontakt został utworzony, ale nie mógł zostać załadowany.");
        }

        return Created($"/api/contacts/{savedUser.Id}/full", savedUser.ToContactDetailsDTO());
    }


    [HttpPut]
    public async Task<ActionResult<ContactDetailsDTO>> UpdateAsync(ContactUpdateDTO contactUpdateDTO)
    {
        if (!IsAdmin() && !IsCurrentUser(contactUpdateDTO.Id))
        {
            return Forbid();
        }

        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        User updatedUser = contactUpdateDTO.ToUser();
        if (!string.IsNullOrWhiteSpace(contactUpdateDTO.Password))
        {
            updatedUser.PasswordHash = _passwordHasher.HashPassword(updatedUser, contactUpdateDTO.Password);
        }

        bool updatedSuccessfully = await _userService.UpdateUserAsync(updatedUser, contactUpdateDTO.CustomSubcategoryName);
        if (!updatedSuccessfully)
        {
            return BadRequest("Kontakt nie istnieje, email jest już w użyciu lub wybrana wartość słownikowa jest nieprawidłowa.");
        }

        var savedUser = await _userService.GetUserByIdAsync(updatedUser.Id);
        if (savedUser == null)
        {
            return Problem("Kontakt został zaktualizowany, ale nie mógł zostać załadowany.");
        }

        return Ok(savedUser.ToContactDetailsDTO());
    }

    [HttpDelete("{id:guid}/delete")]
    [Authorize(Roles = RoleNames.AdminRole)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        bool deletedSuccessfully = await _userService.DeleteUserAsync(id);
        return deletedSuccessfully ? NoContent() : NotFound();
    }

    private bool IsAdmin()
    {
        return User.IsInRole(RoleNames.AdminRole);
    }

    private bool IsCurrentUser(Guid id)
    {
        return Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var currentUserId)
            && currentUserId == id;
    }
}
