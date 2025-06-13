using EventApp.Utility;

namespace EventApp.Models.Dtos;

public class OrderDto
{
    public int Id { get; set; } = 0;
    public int UserId { get; set; } = 0;
    public decimal TotalAmount { get; set; } = 0.0m;
    public string Status { get; set; } = SD.OrderStatusPending;
    public DateTime OrderDate { get; set; } = DateTime.Now;
}

