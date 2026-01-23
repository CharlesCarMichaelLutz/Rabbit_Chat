using api.Data;
using api.Models.Message;
using api.Models.Rooms;
using Dapper;

namespace api.Repositories
{
    public interface IRoomRepository
    {
        Task<bool> CreateGroupAsync(Group group);
        Task<bool> CreatePrivateAsync(Private group);
        Task<bool> AddUserToGroupAsync(User user);
        Task<IEnumerable<MessageResponse>> LoadGroupAsync(int groupId);
        Task<IEnumerable<MessageResponse>> LoadPrivateAsync(int privateId);
    }
    public class RoomRepository : IRoomRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;
        public RoomRepository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<bool> CreateGroupAsync(Group group)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();

            var query = @"INSERT INTO group_chats (group_chat_name, created_at) 
                VALUES (@GroupChatName, @CreatedDate)";

            var result = await connection.ExecuteAsync(query, group);

            return result > 0;

        }
        public async Task<bool> CreatePrivateAsync(Private group)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();

            var query = @"INSERT INTO private_chats (user1_id, user2_id, created_at) 
                VALUES (@UserOneId, @UserTwoId, @CreatedDate)";

            var result = await connection.ExecuteAsync(query, group);

            return result > 0;
        }
        public async Task<bool> AddUserToGroupAsync(User user)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();

            var query = @"INSERT INTO group_chats_detail (user_id, group_chat_id, joined_at) 
                VALUES (@UserId, @GroupChatId, @JoinedAt)";

            var result = await connection.ExecuteAsync(query, user);

            return result > 0;
        }
        public async Task<IEnumerable<MessageResponse>> LoadGroupAsync(int groupId)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();

            const string sql =
                """
                SELECT 
                    u.user_id AS UserId, 
                    m.message_id AS MessageId, 
                    u.username AS UserName, 
                    m.text, 
                    m.created_at AS CreatedDate, 
                    m.is_deleted AS IsDeleted, 
                    m.group_chat_id AS GroupChatId,
                    m.private_chat_id AS PrivateChatId
                FROM messages m 
                JOIN users u ON m.user_id = u.user_id 
                WHERE m.group_chat_id = @GroupChatId 
                ORDER BY m.created_at
                """;

            return await connection.QueryAsync<MessageResponse>(sql, new { GroupChatId = groupId});
        }

        public async Task<IEnumerable<MessageResponse>> LoadPrivateAsync(int privateId)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();

            const string sql =
                """
                SELECT 
                    u.user_id AS UserId, 
                    m.message_id AS MessageId,
                    u.username AS UserName, 
                    m.text, 
                    m.created_at AS CreatedDate,
                    m.is_deleted AS IsDeleted, 
                    m.group_chat_id AS GroupChatId,
                    m.private_chat_id AS PrivateChatId
                FROM messages m 
                JOIN users u ON m.user_id = u.user_id
                WHERE m.private_chat_id = @PrivateChatId
                ORDER BY m.created_at
                """;
            
            return await connection.QueryAsync<MessageResponse>(sql, new { PrivateChatId = privateId });
        }
    }
}
