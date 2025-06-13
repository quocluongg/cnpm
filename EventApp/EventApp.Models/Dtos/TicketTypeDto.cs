namespace EventApp.Models.Dtos;

public class TicketTypeDto
{
    public int Id { get; set; } = 0;
    public int EventId { get; set; } = 0;
    public required string TypeName { get; set; }
    public required decimal BasePrice { get; set; }
    public required int QuantityAvailable { get; set; }
    public required string? Description { get; set; }
}