using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace EventApp.Models
{
    public class Event
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255, ErrorMessage = "The event name must be between 1 and 255 characters long.")]
        public string? TenSuKien { get; set; }
        public string? MoTa { get; set; }
        [Required]
        public DateTime ThoiGianBatDau { get; set; }
        [Required]
        public DateTime ThoiGianKetThuc { get; set; }
        [StringLength(255, ErrorMessage = "The image URL must be between 1 and 255 characters long.")]
        public string? HinhAnh { get; set; }
        [Required]
        public DateTime NgayTao { get; set; }
        
        [Required]
        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public User? User { get; set; }
    }
}
