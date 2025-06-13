using System.ComponentModel.DataAnnotations;

namespace EventApp.Models.Dtos;

public class CategoryDto
{
    public int Id { get; set; } = 0;
    
    [Required]
    public string? CategoryName { get; set; }
}