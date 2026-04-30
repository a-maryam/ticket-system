public class ColumnDto
{
    public int BoardId { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<TicketDto> Tickets = new List<TicketDto>();
}
