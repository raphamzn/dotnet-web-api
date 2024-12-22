namespace WebApi.Models;

public class Todo
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    
    // Foreign key for User
    public int UserId { get; set; }
    public User User { get; set; }
    
}