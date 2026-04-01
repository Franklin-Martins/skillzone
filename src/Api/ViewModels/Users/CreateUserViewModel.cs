using System.ComponentModel.DataAnnotations;

namespace app.Api.ViewModels.Users;

public class CreateUserViewModel
{
    public string Name { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    public string Password { get; set; }
}