// CreateTicketDto.cs

using ticket_system.Enums;

namespace ticket_system.Dtos
{
    public class CreateTicketDto
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
    }
}
