using Microsoft.AspNetCore.Mvc;
using ticket_system.Dtos;
using ticket_system.Services;

namespace ticket_system.Controllers
{
    [ApiController]
    [Route("api/boards")]
    public class BoardController : ControllerBase
    {
        private readonly BoardService _boardService;

        public BoardController(BoardService boardService)
        {
            _boardService = boardService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBoard(CreateBoardDto dto)
        {
            try
            {
                var board = await _boardService.CreateBoard(dto);
                return Ok(board);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBoard(int id)
        {
            var board = await _boardService.GetBoardById(id);

            if (board == null)
                return NotFound();

            return Ok(board);
        }

        [HttpGet("owner/{ownerId}")]
        public async Task<IActionResult> GetBoardsByOwner(int ownerId)
        {
            var boards = await _boardService.GetBoardsByOwner(ownerId);

            return Ok(boards);
        }
    }
}
