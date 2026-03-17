namespace Api.Models;

public class Payment
{
    public int Id { get; set; }
    public EPaymentType Type { get; set; }
    public EPaymentStatus Status { get; set; }
    public int Amount { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? PaidAt { get; set; }
    public Plan Plan { get; set; } = null!;
    public int PlanId { get; set; }
}

public enum EPaymentStatus
{
    Paid = 1,
    Pending = 2,
    Failed = 3,
    Refunded = 4,
    Canceled = 5,
}

public enum EPaymentType
{
    PIX = 1,
    Credit = 2,
    Debit = 3,
    PaymentSlip = 4
}
