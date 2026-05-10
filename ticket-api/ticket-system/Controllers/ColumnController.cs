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
            var column = await _columnService.CreateColumn(dto);
            if (column == null)
                return NotFound();
            return Ok(column);
        }

        [HttpGet("columns/{boardId}")]
        public async Task<IActionResult> GetColumnsByBoard(int boardId)
        {
            var columns = await _columnService.GetColumnsByBoard(boardId);

            if (columns == null)
                return NotFound();

            return Ok(columns);
        }

        [HttpPut("updatename/{columnId}")]
        public async Task<IActionResult> UpdateColumnName(int columnId, UpdateColumnNameDto dto)
        {
            var column = await _columnService.UpdateColumnName(columnId, dto);

            if (column == null)
                return NotFound();

            return Ok(column);
        }

        [HttpDelete("{columnId}")]
        public async Task<IActionResult> DeleteColumn(int columnId)
        {
            await _columnService.DeleteColumn(columnId);
            return NoContent();
        }
    }
}
