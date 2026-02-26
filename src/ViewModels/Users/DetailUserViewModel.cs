using System.ComponentModel.DataAnnotations;

namespace Api.ViewModels.Users;

public class DetailUserViewModel
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Slug { get; set; }
}