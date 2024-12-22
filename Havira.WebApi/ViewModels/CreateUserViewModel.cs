using System.ComponentModel.DataAnnotations;

namespace WebApi.ViewModels;

public class CreateUserViewModel
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}