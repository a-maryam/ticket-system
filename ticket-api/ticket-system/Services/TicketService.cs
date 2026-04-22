using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
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
                var board = await _context.Boards.FirstOrDefaultAsync(b =>
                    b.Id == dto.BoardId.Value
                );
                if (board != null)
                {
                    boardId = board.Id;
                }
                else
                {
                    var newBoard = new Board
                    {
                        Name = dto.NewBoardName ?? "Default Board",
                        OwnerId = 1,
                    };

                    _context.Boards.Add(newBoard);
                    await _context.SaveChangesAsync();

                    boardId = newBoard.Id;
                }
            }
            else if (!string.IsNullOrEmpty(dto.NewBoardName))
            {
                var board = new Board { Name = dto.NewBoardName, OwnerId = 1 };

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
                CreatorId = 1,
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

        public async Task AssignTicket(int ticketId, AssignTicketDto dto)
        {
            var ticket = await _context.Tickets.FindAsync(ticketId);
            var user = await _context.Users.FindAsync(dto.AssigneeId);

            if (ticket == null)
                throw new Exception("Ticket not found.");
            if (user == null)
                throw new Exception("User not found");

            ticket.AssigneeId = user.Id;

            await _context.SaveChangesAsync();
        }

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
                AssigneeId = ticket.AssigneeId,
            };
        }

        public async Task<Board> CreateBoard(CreateBoardDto dto)
        {
            var userExists = await _context.Users.AnyAsync(u => u.Id == dto.OwnerId);

            if(!userExists) throw new Exception("Owner not found");

            var board = new Board { Name = dto.Name, OwnerId = dto.OwnerId };

            _context.Boards.Add(board);
            await _context.SaveChangesAsync();

            return board;
        }
    }
}
