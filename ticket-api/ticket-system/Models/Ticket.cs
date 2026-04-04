namespace ticket_system.Models;

using ticket_system.Enums;

public class Ticket
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public TicketStatus Status { get; set; }
    public required string Description { get; set; }

    public User? Creator { get; set; }
    public int CreatorId { get; set; }

    public User? Assignee { get; set; }
    public int? AssigneeId { get; set; }

    public Board? AssignedBoard { get; set; }
    public int BoardId { get; set; }
}
