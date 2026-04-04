namespace ticket_system.Models;

public class Board
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public required User Owner { get; set; }
    public required int OwnerId { get; set; }

    public required ICollection<Ticket> tickets { get; set; } = new List<Ticket>();
}
