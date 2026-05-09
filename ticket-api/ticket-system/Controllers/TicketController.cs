using Microsoft.AspNetCore.Mvc;
using ticket_system.Dtos;
using ticket_system.Services;

namespace ticket_system.Controllers;

// add board not exists check
[ApiController]
[Route("[controller]")]
public class TicketController : ControllerBase
{
    private readonly TicketService _ticketService;

    public TicketController(TicketService ticketService)
    {
        _ticketService = ticketService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTicket(CreateTicketDto dto)
    {
        var ticket = await _ticketService.CreateTicket(dto);
        return CreatedAtAction(nameof(GetTicketById), new { id = ticket.Id }, ticket);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTicketById(int id)
    {
        var ticket = await _ticketService.GetTicketById(id);

        return ticket == null ? NotFound() : Ok(ticket);
    }

    [HttpPut("{id}/assign")]
    public async Task<IActionResult> AssignTicket(int id, AssignTicketDto dto)
    {
        await _ticketService.AssignTicket(id, dto);
        var ticket = await _ticketService.GetTicketById(id);

        return ticket == null ? NotFound() : Ok(ticket);
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> ChangeTicketStatus(int id, ChangeTicketStatusDto dto)
    {
        var ticket = await _ticketService.ChangeTicketStatus(id, dto);
        return ticket == null ? NotFound() : Ok(ticket);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTicket(int id)
    {
        await _ticketService.DeleteTicket(id);
        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateTicket(int id, UpdateTicketDto dto)
    {
        var ticket = await _ticketService.UpdateTicket(id, dto);
        return Ok(ticket);
    }
}
