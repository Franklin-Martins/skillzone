namespace Api.ViewModels.Plans;

public class DetailPlanViewModel
{
    public int Id { get; set; }

    public Guid? UserId { get; set; }
    public string? UserName { get; set; }

    public bool? IsOwner { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public string Description { get; set; }

    public int? DurationInMonths { get; set; }
    public DateTime? DueDate { get; set; }

    public int? Status { get; set; }

    public DateTime? ExpiresAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }
}