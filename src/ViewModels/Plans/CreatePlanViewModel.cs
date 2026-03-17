using System.ComponentModel.DataAnnotations;
using Api.Models;
using Api.Models.Enums;

namespace Api.ViewModels.Plans;

public class CreatePlanViewModel
{
    [Required]
    public Guid UserId { get; set; }

    public bool? IsOwner { get; set; }

    [Required]
    [MaxLength(120)]
    public string Name { get; set; }

    [Required]
    public int Price { get; set; }

    [MaxLength(500)]
    public string Description { get; set; }

    public int? DurationInMonths { get; set; }

    public DateTime? DueDate { get; set; }

    public EPlanStatus? Status { get; set; }

    public DateTime? ExpiresAt { get; set; }
}