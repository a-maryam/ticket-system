using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ticket_system.Data;
using ticket_system.Dtos;
using ticket_system.Enums;
using ticket_system.Models;

namespace ticket_system.Services;

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
        var column = await _context.Columns.FindAsync(dto.ColumnId);

        if (column == null)
        {
            throw new Exception("Column not found.");
        }

        var position = await _context.Tickets.Where(t => t.ColumnId == column.Id).CountAsync();

        var ticket = new Ticket
        {
            Title = dto.Title,
            Description = dto.Description,
            Status = dto.Status ?? TicketStatus.ToDo,
            CreatorId = 1, // change later
            ColumnId = column.Id,
            Column = column,
            BoardId = column.BoardId,
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
        {
            throw new Exception("Ticket not found.");
        }

        if (user == null)
        {
            throw new Exception("User not found");
        }

        ticket.AssigneeId = user.Id;

        await _context.SaveChangesAsync();
    }

    public async Task<TicketDto?> GetTicketById(int id)
    {
        var ticket = await _context.Tickets.FindAsync(id);
        if (ticket == null)
        {
            return null;
        }

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

    public async Task DeleteTicket(int ticketId)
    {
        var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == ticketId);

        if (ticket == null)
        {
            throw new Exception("Ticket not found.");
        }

        var columnId = ticket.ColumnId;
        var deletedPosition = ticket.Position;

        _context.Tickets.Remove(ticket);

        var tickets = await _context
            .Tickets.Where(t => t.ColumnId == columnId && t.Position > deletedPosition)
            .ToListAsync();

        foreach (var t in tickets)
        {
            t.Position--;
        }

        await _context.SaveChangesAsync();
    }

    public async Task<TicketDto> UpdateTicket(int ticketId, UpdateTicketDto dto)
    {
        var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == ticketId);
        if (ticket == null)
        {
            throw new Exception("Ticket not found.");
        }

        if (dto.Title != null)
        {
            ticket.Title = dto.Title;
        }

        if (dto.Description != null)
        {
            ticket.Description = dto.Description;
        }

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

    public async Task<TicketDto> MoveTicket(int ticketId, MoveTicketDto dto)
    {
        var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == ticketId);
        if (ticket == null)
        {
            throw new Exception("Ticket not found.");
        }

        var sourceColumnId = ticket.ColumnId;
        var sourceTickets = await _context
            .Tickets.Where(t => t.ColumnId == sourceColumnId)
            .OrderBy(t => t.Position)
            .ToListAsync();

        sourceTickets.Remove(ticket);

        for (int i = 0; i < sourceTickets.Count; i++)
        {
            sourceTickets[i].Position = i;
        }

        var targetColumnId = dto.ColumnId;
        var targetTickets = await _context
            .Tickets.Where(t => t.ColumnId == targetColumnId)
            .OrderBy(t => t.Position)
            .ToListAsync();

        if (dto.Position < 0)
            dto.Position = 0;
        if (dto.Position > targetTickets.Count)
            dto.Position = targetTickets.Count;

        ticket.ColumnId = targetColumnId;
        ticket.BoardId = dto.BoardId;

        targetTickets.Insert(dto.Position, ticket);

        for (int i = 0; i < targetTickets.Count; i++)
        {
            targetTickets[i].Position = i;
        }

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
}
