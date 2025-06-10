using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace EventApp.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Organizer")]
        public int? UserId { get; set; }

        [Required, StringLength(255)]
        public string EventName { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Url, StringLength(500)]
        public string? ImageUrl { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [Required, StringLength(200)]
        public string Location { get; set; }

        // Navigation properties
        public User Organizer { get; set; }
        public ICollection<EventCategory> EventCategories { get; set; } = new List<EventCategory>();
        public ICollection<TicketType> TicketTypes { get; set; } = new List<TicketType>();
    }
}
