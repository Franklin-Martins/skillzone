namespace app.Domain.Models;

public class ContentType
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }

    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public IList<Content> Contents { get; set; } = new List<Content>();
}
