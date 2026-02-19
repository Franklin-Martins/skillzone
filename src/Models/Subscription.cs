namespace Api.Models;

public class Subscription
{
    public int Id { get; set; }

    public User User { get; set; } = null;
    public int UserId { get; set; }

    public Membership Membership { get; set; } = null;
    public int MembershipId { get; set; }

    public required DateTime DueDate { get; set; }
    public DateTime ExpiresAt { get; set; }
    public ESubscriptionStatus Status { get; set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
}

public enum ESubscriptionStatus
{
    Pending = 1,
    Active = 2,
    Expired = 3,
    Cancelled = 4
}