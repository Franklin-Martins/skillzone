namespace Api.Models;

public class ContentCategory
{
    public int Id { get; set; }

    public Content Content { get; set; } = null!;
    public int ContentId { get; set; }

    public Category Category { get; set; } = null!;
    public int CategoryId { get; set; }
}
