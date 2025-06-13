using EventApp.Utility;

namespace EventApp.Models.Dtos;

public class TicketDto
{
    public int Id { get; set; } = 0;
    
    public int OrderDetailId { get; set; } = 0;

    public string? TicketCode { get; set; }

    public string Status { get; set; } = SD.TicketStatusValid;
}