using api.Models.Rooms;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _roomService;
        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        // POST /api/rooms/groups
        [HttpPost("groups")]
        public async Task<IActionResult> CreateGroup([FromBody] GroupRequest request)
        {
            var createGroup = await _roomService.CreateGroup(request);

            return Ok(createGroup);
        }

        // POST /api/rooms/chats
        [HttpPost("chats")]
        public async Task<IActionResult> CreateChat([FromBody] PrivateRequest request)
        {
            var createGroup = await _roomService.CreateChat(request);

            return Ok(createGroup);
        }
        // POST /api/rooms/groups/users
        // Consider changing to POST /api/rooms/groups/{groupId}/users and accept groupId explicitly.
        [HttpPost("groups/users")]
        public async Task<IActionResult> AddUserToGroup([FromBody] UserRequest request)
        {
            var addUser = await _roomService.AddUserToGroup(request);

            return Ok(addUser);
        }
        // GET /api/rooms/groups/{id}
        [HttpGet("groups/{Id}")]
        public async Task<IActionResult> LoadGroup([FromRoute]int Id)
        {
            var loadRoom = await _roomService.LoadGroup(Id);

            return Ok(loadRoom);
        }

        // GET /api/rooms/chats/{id}
        [HttpGet("chats/{Id}")]
        public async Task<IActionResult> LoadChat([FromRoute] int Id)
        {
            var loadRoom = await _roomService.LoadChat(Id);

            return Ok(loadRoom);
        }
    }
}
