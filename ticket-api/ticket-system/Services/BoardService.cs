using Microsoft.EntityFrameworkCore;
using ticket_system.Data;
using ticket_system.Dtos;
using ticket_system.Enums;
using ticket_system.Models;

namespace ticket_system.Services
{
    public class BoardService
    {
        private readonly AppDbContext _context;

        public BoardService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<BoardDto> CreateBoard(CreateBoardDto dto)
        {
            var userExists = await _context.Users.AnyAsync(u => u.Id == dto.OwnerId);

            if (!userExists)
                throw new Exception("Owner not found");

            var board = new Board { Name = dto.Name, OwnerId = dto.OwnerId };

            _context.Boards.Add(board);
            await _context.SaveChangesAsync();

            return new BoardDto { Id = board.Id, Name = board.Name };
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
