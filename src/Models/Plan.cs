namespace Api.Models;

public class Plan
{
    public int Id { get; set; }

    public User User { get; set; } = null!;
    public int UserId { get; set; }

    public bool IsOwner { get; set; }
    public required string Name { get; set; }
    public required int Price { get; set; }
    public required string Description { get; set; }
    public required int DurationInMonths { get; set; }
    public required DateTime DueDate { get; set; }
    public EPlanStatus Status { get; set; }
    public DateTime ExpiresAt { get; set; }
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