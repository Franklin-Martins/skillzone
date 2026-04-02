using Api.Models;
using Api.Models.Enums;

namespace Api.ViewModels.Payments;

public class DetailPaymentViewModel
{
    public int Id { get; set;  }

    public int PlanId { get; set; }

    public EPaymentType Type { get; set; }

    public EPaymentStatus Status { get; set; }

    public int Amount { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? PaidAt { get; set; }
}
