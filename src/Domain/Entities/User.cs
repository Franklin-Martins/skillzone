namespace app.Domain.Models;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Slug { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime? LastLoginAt { get; set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public IList<Plan> Plans { get; private set; } = new List<Plan>();
    public IList<Content> Contents { get; private set; } = new List<Content>();
    public IList<Role> Roles { get; set; }
}
