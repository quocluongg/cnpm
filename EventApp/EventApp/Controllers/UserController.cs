using EventApp.DataAccess.Repository.IRepository;
using EventApp.Models;
using EventApp.Models.Dtos;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EventApp.Utility;

namespace EventApp.Controllers;

[ApiController]
[Route("/api/[controller]")]
[Authorize(Roles = SD.AdminRole)]
public class UserController(IUnitOfWork unitOfWork, IConfiguration configuration) : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetAllUser()
    {
        var users = await unitOfWork.Users.GetAllAsync();
        var userDtos = users.Adapt<IEnumerable<UserDto>>();
        return Ok(userDtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await unitOfWork.Users.GetByIdAsync(id);
        if (user == null) return NotFound();
        var userDto = user.Adapt<UserDto>();
        return Ok(userDto);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserDto userDto)
    {
        // if (!ModelState.IsValid) return BadRequest(ModelState);
        var user = userDto.Adapt<User>();
        var result = await unitOfWork.Users.AddAsync(user);
        await unitOfWork.CompleteAsync();
        return Ok(result.Adapt<UserDto>());
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] UserDto userDto)
    {
        User? user = null;
        if (!string.IsNullOrEmpty(userDto.Email))
        {
            var users = await unitOfWork.Users.FindAsync(u => u.Email == userDto.Email && u.Password == userDto.Password);
            user = users.FirstOrDefault();
        }
        else if (!string.IsNullOrEmpty(userDto.Username))
        {
            var users = await unitOfWork.Users.FindAsync(u => u.Username == userDto.Username && u.Password == userDto.Password);
            user = users.FirstOrDefault();
        }

        if (user != null)
        {
            var token = GenerateToken(user);
            return Ok(new { Message = "Login successful", Token = token, User = user.Adapt<UserDto>() });
        }

        return BadRequest("Email or Username is required for login.");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UserDto userDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var existingUser = await unitOfWork.Users.GetByIdAsync(id);
        if (existingUser == null) return NotFound();

        if (userDto.Email != null)
        {
            var existingEmailUser = await unitOfWork.Users.FindAsync(u => u.Email == userDto.Email && u.Id != id);
            if (existingEmailUser.Any())
            {
                return BadRequest("Email already exists.");
            }
        }

        if (userDto.Username != null)
        {
            var existingUserWithUsername = await unitOfWork.Users.FindAsync(u => u.Username == userDto.Username && u.Id != id);
            if (existingUserWithUsername.Any())
            {
                return BadRequest("Username already exists.");
            }

            existingUser.Username = userDto.Username;
        }

        if (userDto.Password != null)
        {
            existingUser.Password = userDto.Password;
        }

        if (userDto.Email != null)
        {
            existingUser.Email = userDto.Email;
        }

        if (userDto.Role != null)
        {
            existingUser.Role = userDto.Role;
        }

        unitOfWork.Users.Update(existingUser);
        await unitOfWork.CompleteAsync();

        return Ok(existingUser.Adapt<UserDto>());
    }

    [HttpPut("{id}/forgot-password")]
    public async Task<IActionResult> ForgotPassword(int id, [FromBody] UserDto userDto)
    {
        var user = await unitOfWork.Users.GetByIdAsync(id);
        if (user == null) return NotFound();

        if (userDto.Password != null)
        {
            user.Password = userDto.Password;
            unitOfWork.Users.Update(user);
            await unitOfWork.CompleteAsync();
            return Ok(new { Message = "Password updated successfully" });
        }

        return BadRequest("Password is required to update.");
    }

    [HttpPut("{id:int}/assign-admin-role")]
    public async Task<IActionResult> AssignAdminRole(int id)
    {
        var user = await unitOfWork.Users.GetByIdAsync(id);
        if (user == null) return NotFound();

        user.Role = SD.AdminRole;
        unitOfWork.Users.Update(user);
        await unitOfWork.CompleteAsync();
        return Ok(new { Message = "User role updated to Admin" });
    }
    
    [HttpPut("{id:int}/assign-user-role")]
    public async Task<IActionResult> AssignUserRole(int id)
    {
        var user = await unitOfWork.Users.GetByIdAsync(id);
        if (user == null) return NotFound();

        user.Role = SD.UserRole;
        unitOfWork.Users.Update(user);
        await unitOfWork.CompleteAsync();
        return Ok(new { Message = "User role updated to User" });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await unitOfWork.Users.GetByIdAsync(id);
        if (user == null) return NotFound();
        unitOfWork.Users.Remove(user);
        await unitOfWork.CompleteAsync();
        return NoContent();
    }
    
    private string GenerateToken(User user)
    {
        var jwtKey = configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not configured");
        var jwtIssuer = configuration["Jwt:Issuer"] ?? throw new InvalidOperationException("JWT Issuer not configured");
        var jwtAudience = configuration["Jwt:Audience"] ?? throw new InvalidOperationException("JWT Audience not configured");
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var token = new JwtSecurityToken(
            issuer: jwtIssuer,
            audience: jwtAudience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}