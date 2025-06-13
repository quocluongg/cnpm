namespace EventApp.Utility;

public static class SD
{
    public const string PaymentStatusPending = "pending";
    public const string PaymentStatusSuccess = "success";
    public const string PaymentStatusFailed = "failed";
    
    public const string PaymentMethodPaypal = "paypal";
    public const string PaymentMethodStripe = "stripe";
    public const string PaymentMethodCash = "cash";
    
    public const string TicketStatusValid = "valid";
    public const string TicketStatusUsed = "used";
    public const string TicketStatusExpired = "expired";
    public const string TicketStatusInvalid = "cancelled";
    
    public const string OrderStatusPending = "pending";
    public const string OrderStatusCompleted = "completed";
    public const string OrderStatusCancelled = "cancelled";
    
    public const string UserRole = "User";
    public const string AdminRole = "Admin";
}