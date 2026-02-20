namespace Api.Models;

public class Content
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public DateTime? UpdatedAt { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public required User User { get; set; }
    public int UserId { get; set; }

    public IList<MembershipContent> Memberships { get; set; } = new List<MembershipContent>();
}