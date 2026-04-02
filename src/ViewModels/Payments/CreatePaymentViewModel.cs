using System.ComponentModel.DataAnnotations;
using Api.Models;
using Api.Models.Enums;

namespace Api.ViewModels.Payments;

public class CreatePaymentViewModel
{
    [Required]
    public int PlanId { get; set; }

    [Required]
    public EPaymentType Type { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int Amount { get; set; }
}
