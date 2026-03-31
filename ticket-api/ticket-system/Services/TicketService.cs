
namespace ticket_system.Services
{
    public class TicketService 
    {
        private readonly AppDbContext _context; 

        public TicketService(AppDbContext context) {
            _context = context;
        }

        public void CreateTicket(int userId) {

        }

        public void AssignTicket(int ticketId, int userId)
        {
            
        }
        
    }
}