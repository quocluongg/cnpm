namespace EventApp.Models.Dtos;

public class PaymentDto
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public DateTime? PaymentTime { get; set; }

    public string Status { get; set; }

    public string? Method { get; set; }

    public required decimal Amount { get; set; }

    public required DateTime ExpiryDate { get; set; }

    public string? Description { get; set; }

    public string? TransactionCode { get; set; }
}