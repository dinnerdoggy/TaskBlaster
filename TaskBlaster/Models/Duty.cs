namespace TaskBlaster.Models;

public class Duty
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool? IsCompleted { get; set; }
    public DateTime? DueDate { get; set; }
    public string Priority { get; set; }

    // User Ownership
    public string Uid { get; set; }

    // Navigation properties
    public int? CategoryId { get; set; }
    public Category? Category { get; set; }
    public List<Comment>? Comments { get; set; }
    public List<Resource>? Resources { get; set; }
}