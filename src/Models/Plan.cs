namespace Api.Models;

public class Plan
{
    public int Id { get; set; }

    public User User { get; set; } = null!;
    public Guid? UserId { get; set; }

    public bool? IsOwner { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public string Description { get; set; }
    public int? DurationInMonths { get; set; }
    public DateTime? DueDate { get; set; }
    public EPlanStatus? Status { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
}

public enum EPlanStatus
{
    Pending = 1,
    Active = 2,
    Expired = 3,
    Canceled = 4
}