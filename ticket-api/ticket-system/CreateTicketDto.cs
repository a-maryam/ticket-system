// CreateTicketDto.cs

namespace ticket_system.Dtos {
    public class CreateTicketDto 
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}