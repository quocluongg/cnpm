using EventApp.DataAccess.Repository.IRepository;
using EventApp.Models;
using EventApp.Models.Dtos;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventApp.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class UserController(IUnitOfWork unitOfWork) : Controller
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
    public async Task<IActionResult> Login([FromBody] UserDto userDto)
    {
        if (!string.IsNullOrEmpty(userDto.Email))
        {
            var users = await unitOfWork.Users.FindAsync(u => u.Email == userDto.Email && u.Password == userDto.Password);
            var userList = users as User[] ?? users.ToArray();
            if (userList.Any())
            {
                var user = userList.FirstOrDefault();
                return Ok(new { Message = "Login successful", User = user.Adapt<UserDto>() });
            }
        }
        else if (!string.IsNullOrEmpty(userDto.Username))
        {
            var users = await unitOfWork.Users.FindAsync(u => u.Username == userDto.Username && u.Password == userDto.Password);
            var userList = users as User[] ?? users.ToArray();
            if (userList.Any())
            {
                var user = userList.FirstOrDefault();
                return Ok(new { Message = "Login successful", User = user.Adapt<UserDto>() });
            }
        }

        return BadRequest("Email or Username is required for login.");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UserDto userDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var existingUser = await unitOfWork.Users.GetByIdAsync(id);
        if (existingUser == null) return NotFound();

        existingUser.Email = userDto.Email;
        existingUser.Username = userDto.Username;
        existingUser.Password = userDto.Password; // Consider hashing the password
        existingUser.FullName = userDto.FullName;
        existingUser.Role = userDto.Role;

        unitOfWork.Users.Update(existingUser);
        await unitOfWork.CompleteAsync();
        
        return Ok(existingUser.Adapt<UserDto>());
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
}