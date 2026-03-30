namespace ticket_system.Models;

public class Comment {
    public int Id { get; set; }
    public User Author { get; set; }
    public int AuthorId { get; set; }
    public string Text { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}