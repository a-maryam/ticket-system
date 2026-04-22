using Microsoft.AspNetCore.Mvc;
using ticket_system.Dtos;
using ticket_system.Services;

namespace ticket_system.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
            var board = await _boardService.CreateBoard(dto);
            return Ok(board);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBoard(int id)
        {
            var board = await _boardService.GetBoardById(id);

            if (board == null)
                return NotFound();

            return Ok(board);
        }
    }
}