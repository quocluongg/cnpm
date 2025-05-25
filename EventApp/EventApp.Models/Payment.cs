using System.ComponentModel.DataAnnotations;
using EventApp.Utility;

namespace EventApp.Models;

public class Payment
{
    public int Id { get; set; }
    [Required]
    public DateTime ThoiGianThucHien { get; set; }

    [Required]
    [StringLength(50, ErrorMessage = "The payment status must be between 1 and 50 characters long.")]
    public string? TrangThai { get; set; } = SD.PaymentStatusPending;
    [Required]
    [StringLength(50, ErrorMessage = "The payment method must be between 1 and 50 characters long.")]
    public string? PhuongThuc { get; set; } = SD.PaymentMethodCash;
    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "The amount must be a positive number.")]
    public decimal SoTien { get; set; } = 0;
    public DateTime NgayKetThuc { get; set; }
    public string? MoTa { get; set; }
}