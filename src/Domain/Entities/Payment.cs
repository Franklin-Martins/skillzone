using Api.Models.Enums;

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
