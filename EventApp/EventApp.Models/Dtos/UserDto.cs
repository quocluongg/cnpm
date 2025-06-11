using System.ComponentModel.DataAnnotations;

namespace EventApp.Models.Dtos;

public class UserDto
{
    [EmailAddress]
    public required string Email { get; set; }

    public required string Username { get; set; }

    public required string Password { get; set; }

    public required string FullName { get; set; }

    public string Role { get; set; } = "User";
}