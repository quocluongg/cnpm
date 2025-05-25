using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventApp.Models;

public class PromotionOrder
{
    [Required]
    public int PromotionId { get; set; }
    [ForeignKey("PromotionId")]
    public Promotion? Promotion { get; set; }
    
    [Required]
    public int OrderId { get; set; }
    [ForeignKey("OrderId")]
    public Order? Order { get; set; }
}