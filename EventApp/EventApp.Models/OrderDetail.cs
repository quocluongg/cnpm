using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventApp.Models;

public class OrderDetail
{
    public int Id { get; set; }
    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "The price must be a positive number.")]
    public decimal GiaVe { get; set; }
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "The quantity must be at least 1.")]
    public int SoLuong { get; set; }
    
    [Required]
    public int OrderId { get; set; }
    [ForeignKey("OrderId")]
    public Order? Order { get; set; }
    
    [Required]
    public int TicketTypeId { get; set; }
    [ForeignKey("TicketTypeId")]
    public TicketType? TicketType { get; set; }
}