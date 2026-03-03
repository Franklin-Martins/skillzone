namespace Api.ViewModels.Contents;

public class CreateContentViewModel {
    public string Name { get; set; }
    public string Description { get; set; }
    public string? Tags { get; set; }
    public bool? IsOwner { get; set; }
}