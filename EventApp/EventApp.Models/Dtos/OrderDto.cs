using EventApp.Utility;

namespace EventApp.Models.Dtos;

public class OrderDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = SD.OrderStatusPending;
    public DateTime OrderDate { get; set; } = DateTime.Now;
}

