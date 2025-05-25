using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EventApp.Utility;
using Microsoft.EntityFrameworkCore;

namespace EventApp.Models;

public class Ticket
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("OrderDetail")]
    public int OrderDetailId { get; set; }

    [Required, StringLength(50)]
    public string TicketCode { get; set; }

    [Required]
    public string Status { get; set; } = SD.TicketStatusValid;

    // Navigation properties
    public OrderDetail OrderDetail { get; set; }
    public Seat Seat { get; set; }
}