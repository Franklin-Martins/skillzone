using app.Domain.Models;
namespace app.Api.ViewModels.ContentTypes;

public class DetailContentTypeViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }

    public IEnumerable<Content> Contents { get; set; }
}
