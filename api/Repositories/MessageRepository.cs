using api.Data;
using api.Models.Message;

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
        private readonly ISqlConnectionFactory _connectionFactory;
        public MessageRepository(ISqlConnectionFactory connectionFactory)
        {
            //inject DB Context here
            _connectionFactory = connectionFactory;
        }
        public async Task<MessageResponse> SaveMessageAsync(Message message)
        {
            //create connection to DB
            //save to DB
            //return to service
            using var connection = await _connectionFactory.CreateConnectionAsync();

            return null;
        }
        public async Task<MessageResponse> GetMessageById(int id)
        {
            //create connection to DB
            //retrieve from DB
            //return to service
            using var connection = await _connectionFactory.CreateConnectionAsync();

            return null;
        }
        public async Task<MessageResponse> SoftDeleteMessage(Message message)
        {
            //create connection to DB
            //save to DB
            //return to service
            using var connection = await _connectionFactory.CreateConnectionAsync();

            return null;
        }
    }
}
