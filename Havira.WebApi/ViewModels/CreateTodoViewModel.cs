using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace WebApi.ViewModels;

public class CreateTodoViewModel
{
    [Required]
    public string Title { get; set; }
    
    public string Description { get; set; }
}