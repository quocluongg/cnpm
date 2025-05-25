using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace EventApp.Models;

public class Seat
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Ticket")]
    public int TicketId { get; set; }

    [Required, StringLength(20)]
    public string SeatCode { get; set; }

    // Navigation property
    [Required]
    public Ticket Ticket { get; set; }
}