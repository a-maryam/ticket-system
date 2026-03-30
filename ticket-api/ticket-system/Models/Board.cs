namespace ticket_system.Models;

public class Board 
{
    public int Id { get; set; }
    public string Name { get; set;}

    public User Owner { get; set; }
    public int OwnerId { get; set; }

    public ICollection<Ticket> tickets { get; set; } = new List<Ticket>();
}