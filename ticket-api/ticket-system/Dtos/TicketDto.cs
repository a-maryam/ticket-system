using ticket_system.Enums;

public class TicketDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public TicketStatus Status { get; set; }

    public int? AssigneeId { get; set; }

    public required int ColumnId { get; set; }

    public int CreatorId { get; set; }

    public int Position { get; set; }
}
