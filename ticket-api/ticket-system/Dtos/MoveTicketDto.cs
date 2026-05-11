namespace ticket_system.Dtos;

public class MoveTicketDto
{
    public required int Position { get; set; }

    public int BoardId { get; set; }

    public required int ColumnId { get; set; }
}
