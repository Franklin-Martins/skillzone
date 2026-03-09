using System.ComponentModel.DataAnnotations;

namespace Api.ViewModels.Auth;

public class LoginAuthViewModel
{
    [Required]
    [EmailAddress]
    public string Email{ get; set; }
    [Required]
    public string Password { get; set; }
}
