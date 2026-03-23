using System.ComponentModel.DataAnnotations;
using Api.Models;
using Api.Models.Enums;

namespace Api.ViewModels.Plans;

public class CreatePlanViewModel
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public int Price { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public int DurationInMonths { get; set; }

    public DateTime? ExpiresAt { get; set; }
}
