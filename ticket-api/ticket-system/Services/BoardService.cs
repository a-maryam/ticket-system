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

            var board = new Board
            {
                Name = dto.Name,
                OwnerId = dto.OwnerId,
                Columns = new List<Column>
                {
                    new Column { Name = "To Do", Position = 0 },
                    new Column { Name = "In Progress", Position = 1 },
                    new Column { Name = "Done", Position = 2 },
                },
            };

            _context.Boards.Add(board);
            await _context.SaveChangesAsync();

            return new BoardDto { Id = board.Id, Name = board.Name };
        }

        public async Task<BoardDto?> GetBoardById(int id)
        {
            var board = await _context
                .Boards.Where(b => b.Id == id)
                .Select(b => new BoardDto
                {
                    Id = b.Id,
                    Name = b.Name,
                    Columns = b
                        .Columns.OrderBy(c => c.Position)
                        .Select(c => new ColumnDto
                        {
                            Id = c.Id,
                            Name = c.Name,
                            Tickets = c
                                .Tickets.OrderBy(t => t.Position)
                                .Select(t => new TicketDto
                                {
                                    Id = t.Id,
                                    Title = t.Title,
                                    CreatorId = t.CreatorId,
                                    Position = t.Position,
                                    ColumnId = t.ColumnId,
                                })
                                .ToList(),
                        })
                        .ToList(),
                })
                .FirstOrDefaultAsync();

            return board;
        }
    }
}
