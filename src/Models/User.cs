namespace Api.Models;

public class User
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public required string Slug { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime? LastLoginAt { get; set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public IList<Plan> Plans { get; private set; } = new();
    public IList<Content> Contents { get; private set; } = new();
}
