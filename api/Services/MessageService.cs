using api.Models.Message;
using api.Repositories;

namespace api.Services
{
    public interface IMessageService
    {
        Task<MessageResponse> CreateMessageAsync(MessageRequest request);
        Task<bool> DeleteMessageAsync(int id);
    }
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }
        public async Task<MessageResponse> CreateMessageAsync(MessageRequest request)
        {
            var message = new Message()
            {
                Text = request.Text,
                UserId = request.UserId,
                GroupChatId = request.GroupChatId,
                PrivateChatId = request.PrivateChatId,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false
            };
            return await _messageRepository.SaveMessageAsync(message);
        }
        public async Task<bool> DeleteMessageAsync(int id)
        {
            var deleteMessage = new Message()
            {
                MessageId = id,
                IsDeleted = true
            };

            return await _messageRepository.SoftDeleteMessage(deleteMessage);
        }
    }
}
