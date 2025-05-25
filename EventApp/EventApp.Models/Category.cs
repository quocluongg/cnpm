using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace EventApp.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string CategoryName { get; set; }

        // Navigation properties
        public ICollection<EventCategory> EventCategories { get; set; } = new List<EventCategory>();
    }
}
