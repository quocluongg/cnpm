using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EventApp.Utility;
using Microsoft.EntityFrameworkCore;

namespace EventApp.Models
{
    [Index(nameof(MaVe), IsUnique = true)]
    public class Ticket
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "The ticket name must be between 1 and 20 characters long.")]
        public string? MaVe { get; set; }
        [Required]
        public string? TrangThai { get; set; } = SD.TicketStatusValid;
        
        [Required]
        public int OrderDetailId { get; set; }
        [ForeignKey("OrderDetailId")]
        public OrderDetail? OrderDetail { get; set; }
        
        [Required]
        public int SeatId { get; set; }
        [ForeignKey("SeatId")]
        public Seat? Seat { get; set; }
    }
}
