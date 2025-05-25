using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventApp.Models;

public class PromotionOrder
{
    [ForeignKey("Promotion")]
    public int PromotionId { get; set; }

    [ForeignKey("Order")]
    public int OrderId { get; set; }

    // Navigation properties
    public Promotion Promotion { get; set; }
    public Order Order { get; set; }
}