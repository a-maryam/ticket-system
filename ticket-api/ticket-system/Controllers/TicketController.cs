using Microsoft.AspNetCore.Mvc;
using ticket_system.Dtos;

namespace ticket_system.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketController : ControllerBase
    {
       [HttpPost]
       public async Task<IActionResult> CreateTicket(CreateTicketDto dto) {
        var ticket = new Ticket 
        {
            Title = dto.Title,
            Description = dto.Description,
            Status = TicketStatus.Open
        };

        _context.Tickets.Add(ticket);

        // persist changes
        await _context.SaveChangesAsync();

        return ticket;
       }
    }
}
