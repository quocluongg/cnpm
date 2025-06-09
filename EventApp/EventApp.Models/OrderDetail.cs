using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventApp.Models;

public class OrderDetail
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Order")]
    public int OrderId { get; set; }

    [ForeignKey("TicketType")]
    public int TicketTypeId { get; set; }

    [Required, Range(0, 10000)]
    [Column(TypeName = "decimal(18,2)")]
    public decimal UnitPrice { get; set; }

    [Required, Range(1, 100)]
    public int Quantity { get; set; }

    // Navigation properties
    public Order Order { get; set; }
    public TicketType TicketType { get; set; }
    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}