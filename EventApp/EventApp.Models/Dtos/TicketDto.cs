using EventApp.Utility;

namespace EventApp.Models.Dtos;

public class TicketDto
{
    public int Id { get; set; }
    
    public int OrderDetailId { get; set; }

    public string TicketCode { get; set; }

    public string Status { get; set; } = SD.TicketStatusValid;
}