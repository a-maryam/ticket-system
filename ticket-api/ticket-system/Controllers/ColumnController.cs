using Microsoft.AspNetCore.Mvc;
using ticket_system.Dtos;
using ticket_system.Services;

namespace ticket_system.Controllers
{
    [ApiController]
    [Route("api/columns")]
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

            return Ok(column);
        }

        [HttpGet("board/{boardId}")]
        public async Task<IActionResult> GetColumnsByBoard(int boardId)
        {
            var columns = await _columnService.GetColumnsByBoard(boardId);

            if (columns == null)
                return NotFound();

            return Ok(columns);
        }

        [HttpPut("{columnId}/name")]
        public async Task<IActionResult> UpdateColumnName(int columnId, UpdateColumnNameDto dto)
        {
            var column = await _columnService.UpdateColumnName(columnId, dto);

            if (column == null)
                return NotFound();

            return Ok(column);
        }

        [HttpPut("{columnId}/move/{newPosition}")]
        public async Task<IActionResult> MoveColumn(int columnId, int newPosition)
        {
            var success = await _columnService.MoveColumn(columnId, newPosition);

            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{columnId}")]
        public async Task<IActionResult> DeleteColumn(int columnId)
        {
            var success = await _columnService.DeleteColumn(columnId);

            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
