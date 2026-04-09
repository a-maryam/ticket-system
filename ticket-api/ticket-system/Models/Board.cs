namespace ticket_system.Models;

public class Board
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public User Owner { get; set; } = null!;
    public int OwnerId { get; set; }

    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
