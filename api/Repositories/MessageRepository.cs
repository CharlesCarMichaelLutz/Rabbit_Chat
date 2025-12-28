using api.Data;
using api.Models.Message;
using Dapper;

namespace api.Repositories
{
    public interface IMessageRepository
    {
        Task<MessageResponse> SaveMessageAsync(Message message);
        Task<MessageResponse> GetMessageById(int id);
        Task<bool> SoftDeleteMessage(Message message);
    }
    public class MessageRepository : IMessageRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;
        public MessageRepository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<MessageResponse> SaveMessageAsync(Message message)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();

            var query = @"INSERT INTO messages (text ,user_id ,group_chat_id ,private_chat_id ,created_at ,is_deleted) 
                VALUES (@Text ,@UserId ,@GroupChatId ,@PrivateChatId ,@CreatedDate ,@IsDeleted)";

            return await connection.QuerySingleAsync<MessageResponse>(query);
        }
        public async Task<MessageResponse> GetMessageById(int id)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();

            var query = @"SELECT * FROM messages WHERE message_id = @MessageId";

            return await connection.QuerySingleAsync<MessageResponse>(query, id);
        }
        public async Task<bool> SoftDeleteMessage(Message message)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();

            var query = @"UPDATE messages SET is_deleted = @IsDeleted WHERE user_id = @UserId ";

            var result = await connection.ExecuteAsync(query, message);

            return result > 0;
        }
    }
}
