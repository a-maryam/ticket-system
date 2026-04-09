// CreateTicketDto.cs

using ticket_system.Enums;

namespace ticket_system.Dtos
{
    public class CreateTicketDto
    {
        public required string Title { get; set; }
        public required string Description { get; set; }

        public int? BoardId { get; set; } // support existing and new board
        public string? NewBoardName { get; set; }

        public int? AssigneeId { get; set; }

        public TicketStatus? Status { get; set; }
    }
}
