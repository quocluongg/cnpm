using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EventApp.Utility;

namespace EventApp.Models;

public class Payment
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Order")]
    public int OrderId { get; set; }

    [Required]
    public DateTime? PaymentTime { get; set; }

    [Required]
    public string Status { get; set; } = SD.PaymentStatusPending;

    [Required, StringLength(50)]
    public string? Method { get; set; }

    [Required, Range(0, 1000000)]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }

    [Required]
    public DateTime ExpiryDate { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    [Required, StringLength(100)]
    public string? TransactionCode { get; set; }

    // Navigation property
    public Order Order { get; set; }
}