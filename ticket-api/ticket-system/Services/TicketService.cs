using Microsoft.EntityFrameworkCore;
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

        public async Task<TicketDto> CreateTicket(CreateTicketDto dto)
        {
            int boardId;

            if (dto.BoardId.HasValue)
            {
                var exists = await _context.Boards.AnyAsync(b => b.Id == dto.BoardId.Value);
                if (!exists)
                    throw new Exception("Board not found.");

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
            return new TicketDto
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Description = ticket.Description,
                BoardId = ticket.BoardId,
                Status = ticket.Status,
            };
        }

        public void AssignTicket(int ticketId, int userId) { }

        public async Task<TicketDto?> GetTicketById(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
                return null;

            return new TicketDto
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Description = ticket.Description,
                BoardId = ticket.BoardId,
                Status = ticket.Status,
            };
        }

        public async Task<BoardDto?> GetBoardById(int id)
        {
            var board = await _context
                .Boards.Include(b => b.Tickets)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (board == null)
                return null;

            return new BoardDto
            {
                Id = board.Id,
                Name = board.Name,
                Tickets = board
                    .Tickets.Select(t => new TicketDto
                    {
                        Id = t.Id,
                        Title = t.Title,
                        Description = t.Description,
                        BoardId = t.BoardId,
                        Status = t.Status,
                    })
                    .ToList(),
            };
        }
    }
}
