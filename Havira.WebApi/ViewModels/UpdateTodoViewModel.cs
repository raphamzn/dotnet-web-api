using System.ComponentModel.DataAnnotations;

namespace WebApi.ViewModels;

public class UpdateTodoViewModel
{
    [Required]
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime? CompletedAt { get; set; }
}