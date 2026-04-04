using ticket_system.Data;
using ticket_system.Dtos;
using ticket_system.Enums;
using ticket_system.Models;

namespace ticket_system.Services
{
    public class TicketService
    {
        private readonly AppDbContext _context;

        public TicketService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Ticket> CreateTicket(CreateTicketDto dto)
        {
            var ticket = new Ticket
            {
                Title = dto.Title,
                Description = dto.Description,
                Status = TicketStatus.ToDo,
                //Creator = ,
                CreatorId = 1, //temporary
                BoardId = 1,
            };

            _context.Tickets.Add(ticket);
            // persist changes
            await _context.SaveChangesAsync();
            return ticket;
        }

        public void AssignTicket(int ticketId, int userId) { }

        public async Task<Ticket?> GetTicketById(int id)
        {
            return await _context.Tickets.FindAsync(id);
        }
    }
}
