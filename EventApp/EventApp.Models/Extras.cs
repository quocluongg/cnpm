using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace EventApp.Models;

[Index(nameof(TenMon), IsUnique = true)]
public class Extras
{
    public int Id { get; set; }
    [Required]
    [StringLength(100, ErrorMessage = "The extra name must be between 1 and 100 characters long.")]
    public string? TenMon { get; set; }
    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "The price must be greater than or equal zero.")]
    public decimal Gia { get; set; } = 0;
    public string? MoTa { get; set; }
}