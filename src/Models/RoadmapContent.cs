namespace Api.Models;

public class RoadmapContent
{
    public int Id { get; set; }

    public Roadmap Roadmap { get; set; } = null!;
    public int RoadmapId { get; set; }

    public Content Content { get; set; } = null!;
    public int ContentId { get; set; }

    public int Order { get; set; }
    public DateTime? CompletedAt { get; set; }
}