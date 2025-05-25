using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventApp.Models;

public class ExtrasOrder
{
    [Required]
    public int ExtrasId { get; set; }
    [ForeignKey("ExtrasId")]
    public Extras? Extras { get; set; }
    
    [Required]
    public int OrderId { get; set; }
    [ForeignKey("OrderId")]
    public Order? Order { get; set; }
}