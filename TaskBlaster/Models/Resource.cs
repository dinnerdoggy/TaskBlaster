namespace TaskBlaster.Models;

public class Resource
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Type { get; set; }
    public string? Url { get; set; }

    // User Ownership
    public string Uid { get; set; }

    // Navigation property
    public List<Duty>? Duties { get; set; }
}
