using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace EventApp.Models
{
    [Index(nameof(TenDanhMuc), IsUnique = true)]
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The category name must be between 1 and 100 characters long.")]
        public string? TenDanhMuc { get; set; }
    }
}
