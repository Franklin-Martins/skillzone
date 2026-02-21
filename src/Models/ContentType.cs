namespace Api.Models;

public class ContentType
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Slug { get; set; }

    public IList<Content> Contents { get; set; } = new List<Content>();
}