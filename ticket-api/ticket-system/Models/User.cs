namespace ticket_system.Models;

public class User
{
    public int Id { get; set; }
    public required string Name { get; set; } = string.Empty;
    public ICollection<Board> Boards { get; set; } = new List<Board>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public ICollection<Ticket> AssignedTickets { get; set; } = new List<Ticket>();
}
