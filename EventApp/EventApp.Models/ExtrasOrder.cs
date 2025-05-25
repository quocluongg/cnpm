using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventApp.Models;

public class ExtrasOrder
{
    [ForeignKey("Extra")]
    public int ExtrasId { get; set; }

    [ForeignKey("Order")]
    public int OrderId { get; set; }

    [Required, Range(1, 100)]
    public int Quantity { get; set; } = 1;

    // Navigation properties
    public Extras Extra { get; set; }
    public Order Order { get; set; }
}