namespace Api.Models;

public class Roadmap
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public DateTime? CompletedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public User User { get; set; } = null!;
    public int UserId { get; set; }

    public IList<RoadmapContent> Contents { get; set; } = new List<RoadmapContent>();
}