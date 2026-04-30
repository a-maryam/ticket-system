using Microsoft.AspNetCore.Mvc;
using ticket_system.Dtos;
using ticket_system.Services;

namespace ticket_system.Controllers
{ 
    [ApiController]
    [Route("[controller]")]
    public class ColumnController : ControllerBase
    {
        private readonly ColumnService _columnService;

        public ColumnController(ColumnService columnService)
        {
            _columnService = columnService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateColumn(CreateColumnDto dto)
        {
            
        }

        [HttpGet]
        public async Task<IActionResult> GetColumnsByBoard()
        {
            
        }
        
    }
}