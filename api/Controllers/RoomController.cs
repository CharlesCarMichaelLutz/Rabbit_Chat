using api.Models.Rooms;
using api.Services;
using Microsoft.AspNetCore.Http;
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

        [HttpPost("create/group")]
        public async Task<IActionResult> CreateGroup([FromBody] GroupRequest request)
        {
            var createGroup = await _roomService.CreateGroupAsync(request);

            return null;
        }

        [HttpPost("create/private")]
        public async Task<IActionResult> CreatePrivate([FromBody] PrivateRequest request)
        {
            var createGroup = await _roomService.CreatePrivateAsync(request);

            return null;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddUserToGroup([FromBody] AddUserRequest request)
        {
            var addUser = await _roomService.AddUserToGroupAsync(request);
            
            return null;
        }

        [HttpGet("load")]
        public async Task<IActionResult> LoadRoom()
        {
            var loadRoom = await _roomService.LoadRoomAsync();

            return null;
        }
    }
}
