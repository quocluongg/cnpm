using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventApp.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        [Required]
        public int EventId { get; set; }
        [Required]
        public int UserId { get; set; }
        
        [ForeignKey("EventId")]
        public Event? Event { get; set; }
        
        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}
