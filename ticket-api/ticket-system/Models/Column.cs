namespace ticket_system.Models;

public class Column
{
    public int Id { get; set; }

    public int BoardId { get; set; }

    public required Board ParentBoard { get; set; }

    public ICollection<Ticket> Tickets = new List<Ticket>();
    
    public int Position { get; set; } // horizontal within board
}
