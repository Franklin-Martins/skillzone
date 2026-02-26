using System.ComponentModel.DataAnnotations;

namespace Api.ViewModels.Users;

public class UpdateUserViewModel
{
    public string? Name { get; set; }
    [EmailAddress]
    public string? Email { get; set; }
    public string? Password { get; set; }
}