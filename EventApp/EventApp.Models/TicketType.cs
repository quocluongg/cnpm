using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace EventApp.Models;

[Index(nameof(TenLoaiVe), IsUnique = true)]
public class TicketType
{
    public int Id { get; set; }
    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "The price must be greater than or equal zero.")]
    public decimal GiaVe { get; set; }
    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "The quantity must be greater than or equal zero.")]
    public int SoLuong { get; set; } = 0;
    public string? MoTa { get; set; }
    [Required]
    [StringLength(100, ErrorMessage = "The ticket type name must be between 1 and 100 characters long.")]
    public string? TenLoaiVe { get; set; }
}