using EventApp.Utility;

namespace EventApp.Models.Dtos;

public class PaymentDto
{
    public int Id { get; set; } = 0;

    public int OrderId { get; set; } = 0;

    public DateTime? PaymentTime { get; set; }

    public string Status { get; set; } = SD.PaymentStatusPending;

    public string Method { get; set; } = SD.PaymentMethodCash;

    public required decimal Amount { get; set; }

    public required DateTime ExpiryDate { get; set; }

    public string? Description { get; set; }

    public string? TransactionCode { get; set; }
}