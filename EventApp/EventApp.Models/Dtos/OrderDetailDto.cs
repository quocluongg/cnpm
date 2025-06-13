namespace EventApp.Models.Dtos;

public class OrderDetailDto
{
    public int Id { get; set; } = 0;

    public int OrderId { get; set; } = 0;

    public int TicketTypeId { get; set; } = 0;
    
    public decimal UnitPrice { get; set; } = 0;

    public int Quantity { get; set; } = 0;
}