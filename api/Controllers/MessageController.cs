using api.Models.Message;
using api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateMessage([FromBody] MessageRequest request)
        {
            var createMessage = await _messageService.CreateMessageAsync(request);

            //propagate new message to all connected clients 
            // _hubcontext.BroadcastSendMessage(createMessage);

            return Ok(createMessage);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            var markDeleted = await _messageService.DeleteMessageAsync(id);

            //propagate new message to all connected clients 
            // _hubcontext.BroadcastDeleteMessage(id);

            return Ok(markDeleted);
        }
    }
}
