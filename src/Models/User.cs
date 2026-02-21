namespace Api.Models;

public class User
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public required string Slug { get; set; }
    public bool IsActive { get; private set; } = true;
    public DateTime? LastLoginAt { get; set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public IList<Subscription> Subscriptions { get; set; } = new List<Subscription>();
    public IList<Content> Contents { get; set; } = new List<Content>();
}
