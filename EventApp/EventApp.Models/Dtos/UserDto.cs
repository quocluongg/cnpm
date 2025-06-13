using System.ComponentModel.DataAnnotations;
using EventApp.Utility;

namespace EventApp.Models.Dtos;

public class UserDto
{
    public int Id { get; set; } = 0;
    [EmailAddress]
    public string? Email { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? FullName { get; set; }
    
    public DateTime CreatedDate { get; set; }

    public string? Role { get; set; } = SD.UserRole;
}