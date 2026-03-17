using Api.Models;

namespace Api.ViewModels.Payments;

public class ListPaymentViewModel
{
    public int Id { get; set; }

    public int PlanId { get; set; }

    public string PlanName { get; set; }

    public EPaymentType Type { get; set; }

    public EPaymentStatus Status { get; set; }

    public int Amount { get; set; }

    public DateTime CreatedAt { get; set; }
}
