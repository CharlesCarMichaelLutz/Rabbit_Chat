using api.Data;
using api.Models.Rooms;
using Dapper;

namespace api.Repositories
{
    public interface IRoomRepository
    {
        //Task<GroupResponse> CreateGroupAsync(Group group);
        Task<bool> CreateGroupAsync(Group group);
        Task<PrivateResponse> CreatePrivateAsync(Private group);
        //Task<UserResponse> AddUserToGroupAsync(User user);
        Task<bool> AddUserToGroupAsync(User user);

        Task<IEnumerable<GroupMessage>> LoadGroupAsync(int groupId);
        Task<IEnumerable<PrivateMessage>> LoadPrivateAsync(int privateId);
    }
    public class RoomRepository : IRoomRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;
        public RoomRepository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        //public async Task<GroupResponse> CreateGroupAsync(Group group)
        //{
        //    using var connection = await _connectionFactory.CreateConnectionAsync();

        //    var query = @"INSERT INTO group_chats (group_chat_name, created_at) 
        //        VALUES (@GroupChatName, @CreatedDate)";

        //    return await connection.QuerySingleAsync<GroupResponse>(query, group);
        //}
        public async Task<bool> CreateGroupAsync(Group group)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();

            var query = @"INSERT INTO group_chats (group_chat_name, created_at) 
                VALUES (@GroupChatName, @CreatedDate)";

            var result = await connection.ExecuteAsync(query, group);

            return result > 0;

        }
        public async Task<PrivateResponse> CreatePrivateAsync(Private group)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();

            var query = @"INSERT INTO private_chats (user1_id, user2_id, created_at) 
                VALUES (@UserOneId, @UserTwoId, @CreatedDate)";

            return await connection.QuerySingleAsync<PrivateResponse>(query, group);
        }
        //public async Task<UserResponse> AddUserToGroupAsync(User user)
        //{
        //    using var connection = await _connectionFactory.CreateConnectionAsync();

        //    var query = @"INSERT INTO group_chats_detail (user_id, group_chat_id, joined_at) 
        //        VALUES (@UserId, @GroupChatId, @JoinedAt)";

        //    return await connection.QuerySingleAsync<UserResponse>(query, user);
        //}
        public async Task<bool> AddUserToGroupAsync(User user)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();

            var query = @"INSERT INTO group_chats_detail (user_id, group_chat_id, joined_at) 
                VALUES (@UserId, @GroupChatId, @JoinedAt)";

            var result = await connection.ExecuteAsync(query, user);

            return result > 0;
        }

        public async Task<IEnumerable<GroupMessage>> LoadGroupAsync(int groupId)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();

            var query = 
                @"SELECT u.username, m.text, m.created_at, m.is_deleted
                FROM messages m 
                JOIN users u ON m.user_id = u.user_id 
                WHERE m.group_chat_id = @GroupChatId 
                ORDER BY m.created_at";

            return await connection.QueryAsync<GroupMessage>(query, groupId);
        }
        public async Task<IEnumerable<PrivateMessage>> LoadPrivateAsync(int privateId)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();

            var query =
                @"SELECT u.username, m.text, m.created_at 
                FROM messages m JOIN users u ON m.user_id = u.user_id
                WHERE m.private_chat_id = @PrivateChatId
                ORDER BY m.created_at";
            
            return await connection.QueryAsync<PrivateMessage>(query, privateId);
        }
    }
}
