using Microsoft.EntityFrameworkCore;
using ticket_system.Data;
using ticket_system.Dtos;
using ticket_system.Enums;
using ticket_system.Models;

namespace ticket_system.Services;

public class ColumnService
{
    private readonly AppDbContext _context;

    public ColumnService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Column?> CreateColumn(CreateColumnDto dto)
    {
        var maxPosition =
            await _context
                .Columns.Where(c => c.BoardId == dto.BoardId)
                .Select(c => (int?)c.Position)
                .MaxAsync()
            ?? -1;

        var column = new Column
        {
            Name = dto.Name,
            BoardId = dto.BoardId,
            Position = maxPosition + 1,
        };

        _context.Columns.Add(column);
        await _context.SaveChangesAsync();

        return column;
    }

    public async Task<List<Column>?> GetColumnsByBoard(int boardId)
    {
        var columns = await _context
            .Columns.Where(c => c.BoardId == boardId)
            .OrderBy(c => c.Position)
            .Include(c => c.Tickets)
            .ToListAsync();

        if (columns.Count == 0)
            return null;

        return columns;
    }

    public async Task<Column?> UpdateColumnName(int columnId, UpdateColumnNameDto dto)
    {
        var column = await _context.Columns.FindAsync(columnId);

        if (column == null)
            return null;

        column.Name = dto.Name;

        await _context.SaveChangesAsync();

        return column;
    }

    public async Task<bool> MoveColumn(int columnId, int newPosition)
    {
        var column = await _context.Columns.FindAsync(columnId);
        if (column == null)
            return false;

        var boardId = column.BoardId;

        var columns = await _context
            .Columns.Where(c => c.BoardId == boardId)
            .OrderBy(c => c.Position)
            .ToListAsync();

        if (newPosition < 0 || newPosition >= columns.Count)
            return false;

        columns.Remove(column);

        columns.Insert(newPosition, column);

        for (int i = 0; i < columns.Count; i++)
        {
            columns[i].Position = i;
        }

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteColumn(int columnId)
    {
        var column = await _context
            .Columns.Include(c => c.Tickets)
            .FirstOrDefaultAsync(c => c.Id == columnId);

        if (column == null)
            return false;

        var boardId = column.BoardId;

        _context.Columns.Remove(column);
        await _context.SaveChangesAsync();

        var columns = await _context
            .Columns.Where(c => c.BoardId == boardId)
            .OrderBy(c => c.Position)
            .ToListAsync();

        for (int i = 0; i < columns.Count; i++)
        {
            columns[i].Position = i;
        }

        await _context.SaveChangesAsync();

        return true;
    }
}
