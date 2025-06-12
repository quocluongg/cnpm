namespace EventApp.Models.Dtos;

public class OrderDetailDto
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int TicketTypeId { get; set; }
    
    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }
}