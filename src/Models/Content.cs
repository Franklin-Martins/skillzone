namespace Api.Models;

public class Content
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public DateTime? UpdatedAt { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    
    public User User { get; set; } = null!;
    public int UserId { get; set; }

    public ContentType ContentType { get; set; } = null!;
    public int ContentTypeId { get; set; }

    public IList<MembershipContent> Memberships { get; set; } = new List<MembershipContent>();
}