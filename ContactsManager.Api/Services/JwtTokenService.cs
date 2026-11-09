using ContactsManager.Api.Models;
using ContactsManager.Shared.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ContactsManager.Api.Services;

public class JwtTokenService
{
    private readonly IConfiguration _configuration;
    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// Creates a JWT access token for the specified user.
    /// </summary>
    /// <remarks>The token expires 60 minutes after creation.</remarks>
    /// <param name="user">The user whose identity and role are included in the token.</param>
    /// <returns>A login response containing the signed JWT and its expiration time.</returns>
    public LoginResponseDTO CreateToken(User user)
    {
        // Creating claims for the token
        // based on user information (Id, Email, Role)
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, user.Role)
        };

        // Creating the signing key and credentials for the token
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddMinutes(60);       // shorter expiration time = better security

        // Creating the JWT token
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: expires,
            signingCredentials: credentials);

        return new LoginResponseDTO
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            ExpireTime = expires
        };
    }

}
