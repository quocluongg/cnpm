using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EventApp.Utility;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace EventApp.Models;

public class Order
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("User")]
    public int UserId { get; set; }

    [Required, Range(0, 1000000)]
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; }

    [Required]
    public string Status { get; set; } = SD.OrderStatusPending;

    [Required]
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public User User { get; set; }
    public Payment Payment { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    public ICollection<PromotionOrder> PromotionOrders { get; set; } = new List<PromotionOrder>();
    public ICollection<ExtrasOrder> ExtrasOrders { get; set; } = new List<ExtrasOrder>();
}