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
            int boardId;

            if (dto.BoardId.HasValue)
            {
                boardId = dto.BoardId.Value;
            }
            else if (!string.IsNullOrEmpty(dto.NewBoardName))
            {
                var board = new Board
                {
                    Name = dto.NewBoardName,
                    OwnerId = 1, //temp
                };
                _context.Boards.Add(board);
                await _context.SaveChangesAsync();
                boardId = board.Id;
            }
            else
            {
                throw new Exception("Either BoardId or NewBoardName required.");
            }

            var ticket = new Ticket
            {
                Title = dto.Title,
                Description = dto.Description,
                Status = dto.Status ?? TicketStatus.ToDo,
                //Creator = ,
                CreatorId = 1, //temporary
                BoardId = boardId,
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
