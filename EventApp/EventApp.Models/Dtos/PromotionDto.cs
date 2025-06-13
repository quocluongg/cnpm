namespace EventApp.Models.Dtos;

public class PromotionDto
{
    public int Id { get; set; } = 0;

    public string? PromotionCode { get; set; }

    public decimal DiscountPercent { get; set; } = 0;

    public int UsageLimit { get; set; } = 0;

    public DateTime StartDate { get; set; } = DateTime.UtcNow;

    public DateTime EndDate { get; set; } = DateTime.UtcNow.AddDays(30);

    public string? Description { get; set; } = null;
    
    public ICollection<int>? OrderIds { get; set; }
}