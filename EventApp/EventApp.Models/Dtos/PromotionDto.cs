namespace EventApp.Models.Dtos;

public class PromotionDto
{
    public int Id { get; set; }

    public string PromotionCode { get; set; }

    public decimal DiscountPercent { get; set; }

    public int UsageLimit { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string? Description { get; set; }
    
    public ICollection<int>? OrderIds { get; set; }
}