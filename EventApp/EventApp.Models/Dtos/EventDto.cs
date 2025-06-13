using System.ComponentModel.DataAnnotations;

namespace EventApp.Models.Dtos;

public class EventDto
{
    public int Id { get; set; } = 0;
        
    public required int UserId { get; set; } = 0;
        
    [Required]
    [MaxLength(255)]
    public string? EventName { get; set; }
        
    [MaxLength(1000)]
    public string? Description { get; set; }
        
    [Required]
    public DateTime? StartTime { get; set; }
        
    [Required]
    public DateTime? EndTime { get; set; }
        
    [Url]
    [MaxLength(500)]
    public string? ImageUrl { get; set; }
        
    [Required]
    public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
        
    [Required]
    [MaxLength(200)]
    public string? Location { get; set; }
}