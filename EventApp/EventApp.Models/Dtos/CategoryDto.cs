using System.ComponentModel.DataAnnotations;

namespace EventApp.Models.Dtos;

public class CategoryDto
{
    [Required]
    public string CategoryName { get; set; }
}