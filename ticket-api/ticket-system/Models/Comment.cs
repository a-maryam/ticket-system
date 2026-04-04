namespace ticket_system.Models;

public class Comment
{
    public int Id { get; set; }
    public required User Author { get; set; }
    public required int AuthorId { get; set; }
    public required string Text { get; set; } = string.Empty;
    public required DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
