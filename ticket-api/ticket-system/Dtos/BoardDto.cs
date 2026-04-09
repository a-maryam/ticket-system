using ticket_system.Dtos;

public class BoardDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<TicketDto> Tickets { get; set; } = new List<TicketDto>();
}
