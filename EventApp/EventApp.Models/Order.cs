using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace EventApp.Models;

public class Order
{
    public int Id { get; set; }
    [Required]
    public decimal TongTien { get; set; }
    [Required]
    [StringLength(50, ErrorMessage = "The status must be between 1 and 50 characters long.")]
    public string? TrangThai { get; set; }
    [Required]
    public DateTime NgayDat { get; set; }
    
    [Required]
    public int UserId { get; set; }
    [ForeignKey("UserId")]
    [ValidateNever]
    public User? User { get; set; }

    public int? PaymentId { get; set; } = null;
    [ForeignKey("PaymentId")]
    [ValidateNever]
    public Payment? Payment { get; set; }
}