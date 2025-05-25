using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace EventApp.Models;

[Index(nameof(MaGiamGia), IsUnique = true)]
public class Promotion
{
    public int Id { get; set; }
    [Required, StringLength(100, ErrorMessage = "The promotion name must be between 1 and 100 characters long.")]
    public string? MaGiamGia { get; set; }
    [Required]
    [Range(1, 100, ErrorMessage = "The discount percentage must be between 1 and 100.")]
    public int PhanTram { get; set; }
    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "The maximum discount must be a non-negative number.")]
    public int SoLanDung { get; set; }
    [Required]
    public DateTime NgayBatDau { get; set; }
    [Required]
    public DateTime NgayKetThuc { get; set; }
    public string? MoTa { get; set; }
}