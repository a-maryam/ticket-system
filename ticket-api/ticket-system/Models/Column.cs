namespace ticket_system.Models;

public class Column
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int BoardId { get; set; }

    public Board? Board { get; set; }

    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public int Position { get; set; } // horizontal within board
}
