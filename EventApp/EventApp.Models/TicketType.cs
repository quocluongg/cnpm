using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EventApp.Models;

public class TicketType
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Event")]
    public int EventId { get; set; }

    [Required, StringLength(100)]
    public string TypeName { get; set; }

    [Required, Range(0, 10000)]
    [Column(TypeName = "decimal(18,2)")]
    public decimal BasePrice { get; set; }

    [Required, Range(0, 100000)]
    public int QuantityAvailable { get; set; }

    [StringLength(500)]
    public string Description { get; set; }

    // Navigation properties
    public Event Event { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; }
    public ICollection<Seat> Seats { get; set; }
}