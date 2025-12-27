using api.Models.Message;
using api.Services;
using System.Reflection.Metadata.Ecma335;

namespace api.Repositories
{
    public interface IMessageRepository
    {
        Task<MessageResponse> SaveMessageAsync(Message message);
        Task<MessageResponse> GetMessageById(int id);
        Task<MessageResponse> SoftDeleteMessage(Message message);
    }
    public class MessageRepository : IMessageRepository
    {
        public MessageRepository()
        {
            //inject DB Context here
        }
        public async Task<MessageResponse> SaveMessageAsync(Message message)
        {
            //create connection to DB
            //save to DB
            //return to service
            return null;
        }
        public async Task<MessageResponse> GetMessageById(int id)
        {
            //create connection to DB
            //retrieve from DB
            //return to service
            return null;
        }
        public async Task<MessageResponse> SoftDeleteMessage(Message message)
        {
            //create connection to DB
            //save to DB
            //return to service
            return null;
        }
    }
}
