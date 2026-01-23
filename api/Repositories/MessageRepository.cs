using api.Data;
using api.Models.Message;
using Dapper;

namespace api.Repositories
{
    public interface IMessageRepository
    {
        Task<MessageResponse> SaveMessageAsync(Message message);
        //Task<bool> SoftDeleteMessage(Message message);
        Task<bool> SoftDeleteMessage(int id, bool delete);

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

            const string sql =
                """
                INSERT INTO messages 
                    (text, user_id, username, group_chat_id, private_chat_id, created_at, is_deleted) 
                VALUES 
                    (@Text, @UserId, @UserName, @GroupChatId, @PrivateChatId, @CreatedDate, @IsDeleted) 
                RETURNING 
                    message_id AS "MessageId", text AS "Text", user_id AS "UserId", username AS "UserName", group_chat_id AS "GroupChatId", private_chat_id AS "PrivateChatId", created_at AS "CreatedDate", is_deleted AS "IsDeleted"
                """;

            return await connection.QuerySingleAsync<MessageResponse>(sql, message);
        }
        //public async Task<bool> SoftDeleteMessage(Message message)
        //{
        //    using var connection = await _connectionFactory.CreateConnectionAsync();

        //    var query = @"UPDATE messages SET is_deleted = @IsDeleted WHERE user_id = @UserId ";

        //    var result = await connection.ExecuteAsync(query, message);

        //    return result > 0;
        //}
        public async Task<bool> SoftDeleteMessage(int id, bool delete)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();

            var query = @"UPDATE messages SET is_deleted = @IsDeleted WHERE message_id = @MessageId ";

            var result = await connection.ExecuteAsync(query, new { MessageId = id, IsDeleted = delete });

            return result > 0;
        }
    }
}
