using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EventApp.Utility;

namespace EventApp.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, EmailAddress, StringLength(255)]
        public string Email { get; set; }

        [Required, StringLength(50, MinimumLength = 3)]
        public string Username { get; set; }

        [Required, StringLength(100, MinimumLength = 8)]
        public string Password { get; set; }

        [Required, StringLength(100)]
        public string FullName { get; set; }

        [Required, StringLength(20)]
        public string Role { get; set; } = SD.UserRole;

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ICollection<Event> OrganizedEvents { get; set; } = new List<Event>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
