using System.ComponentModel.DataAnnotations;

namespace app.Api.ViewModels.Users;

public class DetailUserViewModel
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Slug { get; set; }
}