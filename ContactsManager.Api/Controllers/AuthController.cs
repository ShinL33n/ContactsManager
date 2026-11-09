using ContactsManager.Api.Data;
using ContactsManager.Api.Models;
using ContactsManager.Api.Services;
using ContactsManager.Api.Services.IServices;
using ContactsManager.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

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
        var user = await _userService.GetUserByEmailAsync(loginRequest.Email);

        if(user == null)
        {
            return Unauthorized("Invalid email or password.");
        }

        var isPasswordValid = _hasher.VerifyHashedPassword(user, user.PasswordHash, loginRequest.Password) != PasswordVerificationResult.Failed;

        if(!isPasswordValid)
        {
            return Unauthorized("Invalid email or password.");
        }

        return Ok(_jwtToken.CreateToken(user));
    }
}
