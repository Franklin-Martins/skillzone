using Api.Models.Enums;

namespace Api.Models;

public class PlanStatusHistory
{
    public int Id { get; set; }

    public int PlanId { get; set; }
    public Plan Plan { get; set; } = null!;

    public EPlanStatus Status { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
