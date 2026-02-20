namespace Api.Models;

public class Membership
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required int Price { get; set; }
    public string? Description { get; set; }
    public required int DurationInMonths { get; set; }
    public bool IsActive { get; private set; } = false;
    public DateTime? UpdatedAt { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public IList<Subscription> Subscriptions { get; set; } = new List<Subscription>();
    public IList<MembershipContent> Contents { get; set; } = new List<MembershipContent>();
}