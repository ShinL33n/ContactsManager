using ContactsManager.Api.Mappings;
using ContactsManager.Api.Models;
using ContactsManager.Api.Services;
using ContactsManager.Api.Services.IServices;
using ContactsManager.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ContactsManager.Api.Controllers;

[ApiController] [Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IPasswordHasher<User> _hasher;
    private readonly JwtTokenService _jwtToken;


    public AuthController(IUserService userService, 
                          IPasswordHasher<User> hasher,
                          JwtTokenService jwtToken)
    {
        _userService = userService;
        _hasher = hasher;
        _jwtToken = jwtToken;
    }

    [HttpPost("login")] [AllowAnonymous]
    public async Task<ActionResult<LoginResponseDTO>> Login(LoginRequestDTO loginRequest)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.GetUserByEmailAsync(loginRequest.Email);

                if (user == null)
                {
                    return Unauthorized("Invalid email or password.");
                }

                var isPasswordValid = _hasher.VerifyHashedPassword(user, user.PasswordHash, loginRequest.Password) != PasswordVerificationResult.Failed;

                if (!isPasswordValid)
                {
                    return Unauthorized("Invalid email or password.");
                }

                return Ok(_jwtToken.CreateToken(user));
            }
            else
            {
                return BadRequest("Invalid request data.");
            }
        }
        catch (Exception )
        {
            return StatusCode(500, "An error occurred.");
        }
    }

    [HttpPost("register")] [AllowAnonymous]
    public async Task<ActionResult> Register(RegisterRequestDTO registerRequest)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userService.GetUserByEmailAsync(registerRequest.Email);
                if (existingUser != null)
                {
                    return BadRequest("Email is already in use.");
                }

                User newUser = registerRequest.ToUser();

                newUser.PasswordHash = _hasher.HashPassword(newUser, registerRequest.Password);

                var created = await _userService.CreateUserAsync(newUser);
                if (!created)
                {
                    return StatusCode(500, "An error occurred while creating the user.");
                }

                return Ok(_jwtToken.CreateToken(newUser));
            }
            else
            {
                return BadRequest("Invalid request data.");
            }
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred.");
        }
    }
}
