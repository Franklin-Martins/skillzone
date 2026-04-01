namespace app.Domain.Models;

public class Content
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string? Tags { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public bool? IsOwner { get; set; }
    
    public User? User { get; set; } = null!;
    public Guid? UserId { get; set; }
    
    public ContentType? ContentType { get; set; } = null!;
    public int? ContentTypeId { get; set; }
}
