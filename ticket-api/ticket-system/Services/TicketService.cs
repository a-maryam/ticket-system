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

        // check that this is correct
        public async Task<TicketDto> CreateTicket(CreateTicketDto dto)
        {
            var column = await _context
                .Columns.Include(c => c.Tickets)
                .FirstOrDefaultAsync(c => c.Id == dto.ColumnId);

            if (column == null)
                throw new Exception("Column not found.");

            var position = await _context
                .Tickets.Where(t => t.ColumnId == dto.ColumnId)
                .CountAsync();

            var ticket = new Ticket
            {
                Title = dto.Title,
                Description = dto.Description,
                Status = dto.Status ?? TicketStatus.ToDo,
                CreatorId = 1, // change later
                ColumnId = column.Id,
                Column = column,
                Position = position,
            };

            _context.Tickets.Add(ticket);

            await _context.SaveChangesAsync();

            return new TicketDto
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Description = ticket.Description,
                ColumnId = ticket.ColumnId,
                Status = ticket.Status,
                CreatorId = ticket.CreatorId,
                Position = ticket.Position,
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
                ColumnId = ticket.ColumnId,
                Status = ticket.Status,
                CreatorId = ticket.CreatorId,
                Position = ticket.Position,
            };
        }

        public async Task<TicketDto?> ChangeTicketStatus(int id, ChangeTicketStatusDto dto)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return null;
            }

            ticket.Status = dto.Status;
            _ = await _context.SaveChangesAsync();

            return new TicketDto
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Description = ticket.Description,
                ColumnId = ticket.ColumnId,
                Status = ticket.Status,
                CreatorId = ticket.CreatorId,
                Position = ticket.Position,
            };
        }
    }
}
