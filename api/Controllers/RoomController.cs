using api.Models.Rooms;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpPost("creategroup")]
        public async Task<IActionResult> CreateGroup([FromBody] GroupRequest request)
        {
            var createGroup = await _roomService.CreateGroupAsync(request);

            return Ok(createGroup);
        }

        [HttpPost("createprivate")]
        public async Task<IActionResult> CreatePrivate([FromBody] PrivateRequest request)
        {
            var createGroup = await _roomService.CreatePrivateAsync(request);

            return Ok(createGroup);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddUserToGroup([FromBody] UserRequest request)
        {
            var addUser = await _roomService.AddUserToGroupAsync(request);

            return Ok(addUser);
        }

        [HttpGet("loadgroup/{groupId}")]
        public async Task<IActionResult> LoadGroup(int groupId)
        {
            var loadRoom = await _roomService.LoadGroup(groupId);

            return Ok(loadRoom);
        }

        [HttpGet("loadprivate/{privateId}")]
        public async Task<IActionResult> LoadPrivate(int privateId)
        {
            var loadRoom = await _roomService.LoadPrivate(privateId);

            return Ok(loadRoom);
        }
    }
}
