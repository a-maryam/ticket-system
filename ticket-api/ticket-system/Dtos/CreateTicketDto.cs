// CreateTicketDto.cs

using ticket_system.Enums;

namespace ticket_system.Dtos
{
    public class CreateTicketDto
    {
        public required string Title { get; set; }

        public string Description { get; set; } = string.Empty;

        public int BoardId { get; set; }

        public required int ColumnId { get; set; }

        public string? NewBoardName { get; set; }

        public int? AssigneeId { get; set; }

        public TicketStatus? Status { get; set; }
    }
}
