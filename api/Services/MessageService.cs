using api.Models.Message;
using api.Repositories;

namespace api.Services
{
    public interface IMessageService
    {
        Task<MessageResponse> CreateMessageAsync(MessageRequest request);
        Task<MessageResponse> DeleteMessageAsync(int id);
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
        public async Task<MessageResponse> DeleteMessageAsync(int id)
        {
            var getMessage = await _messageRepository.GetMessageById(id);

            var deleteMessage = new Message()
            {
                Text = getMessage.Text,
                UserId = getMessage.UserId,
                GroupChatId = getMessage.GroupChatId,
                PrivateChatId = getMessage.PrivateChatId,
                CreatedDate = getMessage.CreatedDate,
                IsDeleted = true
            };
            return await _messageRepository.SoftDeleteMessage(deleteMessage);
        }
    }
}

//Room Service
//    get all messages group
//    get all messages private
//public async Task<MessageResponse> GetMessageAsync()
//{
//    return null;
//}