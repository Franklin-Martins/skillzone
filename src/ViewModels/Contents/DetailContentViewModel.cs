namespace Api.ViewModels.Contents;

public class DetailContentViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? Tags { get; set; }
    public DateTime CreatedAt { get; set; }

    public string? Username { get; set; }
    public Guid? UserId { get; set; }
    
    public string? ContentTypeName { get; set; }
    public int? ContentTypeId { get; set; }
}