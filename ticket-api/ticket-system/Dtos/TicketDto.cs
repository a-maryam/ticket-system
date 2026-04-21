using ticket_system.Enums;
public class TicketDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int BoardId { get; set; }
    public string Description { get; set; } = string.Empty;
    public TicketStatus Status { get; set; }
    public int? AssigneeId { get; set; }
}
