using app.Domain.Models.Enums;

namespace app.Domain.Models;

public class PlanStatusHistory
{
    public int Id { get; set; }
    public int PlanId { get; set; }
    public string Description { get; set; } = null!;
    public int Price{ get; set; }
    public string OwnerEmail { get; set; } = null!;
    public EPlanStatus Status { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
