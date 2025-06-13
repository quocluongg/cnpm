using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EventApp.Models;

public class Promotion
{
    [Key]
    public int Id { get; set; }

    [Required, StringLength(50)]
    public string PromotionCode { get; set; }

    [Required, Range(0, 100)]
    [Column(TypeName = "decimal(18,2)")]
    public decimal DiscountPercent { get; set; }

    [Required, Range(0, 1000)]
    public int UsageLimit { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    // Navigation property
    public ICollection<PromotionOrder> PromotionOrders { get; set; } = new List<PromotionOrder>();
}