public class ColumnDto
{
    public int Id { get; set; }
    public int BoardId { get; set; }
    public string Name { get; set; } = string.Empty;

    public int Position { get; set; }
    public List<TicketDto> Tickets = new List<TicketDto>();
}
