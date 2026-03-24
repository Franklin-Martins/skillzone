namespace Api.ViewModels.ContentTypes;

public class ItemListContentTypeViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }
}