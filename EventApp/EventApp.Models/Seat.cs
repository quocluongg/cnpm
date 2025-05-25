using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace EventApp.Models;

public class Seat
{
    public int Id { get; set; }
    [Required]
    [StringLength(10, ErrorMessage = "The seat name must be between 1 and 10 characters long.")]
    public string? MaGhe { get; set; }
    
    [Required]
    public int TicketTypeId { get; set; }
    [ForeignKey("TicketTypeId")]
    [ValidateNever]
    public TicketType? TicketType { get; set; }
}