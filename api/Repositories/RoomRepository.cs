using api.Data;
using api.Models.Rooms;
using Dapper;

namespace api.Repositories
{
    public interface IRoomRepository
    {
        Task<GroupResponse> CreateGroupAsync(Group group);
        Task<PrivateResponse> CreatePrivateAsync(Private group);
        Task<AddUserResponse> AddUserToGroupAsync(AddUser user);
        Task<IEnumerable<Gmsg>> LoadGroupAsync(int groupId);
        Task<IEnumerable<Pmsg>> LoadPrivateAsync(int privateId);
    }
    public class RoomRepository : IRoomRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;
        public RoomRepository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<GroupResponse> CreateGroupAsync(Group group)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();

            var query = @"INSERT INTO group_chats (group_chat_name, is_private, created_at) 
                VALUES (@GroupChatName, @IsPrivate, @CreatedDate)";

            return await connection.QuerySingleAsync<GroupResponse>(query, group);
        }
        public async Task<PrivateResponse> CreatePrivateAsync(Private group)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();

            var query = @"INSERT INTO private_chats (user1_id, user2_id, created_at) 
                VALUES (@UserOneId, @UserTwoId, @CreatedDate)";

            return await connection.QuerySingleAsync<PrivateResponse>(query, group);
        }
        public async Task<AddUserResponse> AddUserToGroupAsync(AddUser user)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();

            var query = @"INSERT INTO group_chats_detail (user_id, group_chat_id, joined_at) 
                VALUES (@UserId, @GroupChatId, @JoinedAt)";

            return await connection.QuerySingleAsync<AddUserResponse>(query, user);
        }
        public async Task<IEnumerable<Gmsg>> LoadGroupAsync(int groupId)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();

            var query = 
                @"SELECT u.username, m.text, m.created_at 
                FROM messages m 
                JOIN users u ON m.user_id = u.user_id 
                WHERE m.group_chat_id = @GroupChatId 
                ORDER BY m.created_at";

            return await connection.QueryAsync<Gmsg>(query, groupId);
        }
        public async Task<IEnumerable<Pmsg>> LoadPrivateAsync(int privateId)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();

            var query =
                @"SELECT u.username, m.text, m.created_at 
                FROM messages m JOIN users u ON m.user_id = u.user_id
                WHERE m.private_chat_id = @PrivateChatId
                ORDER BY m.created_at";
            
            return await connection.QueryAsync<Pmsg>(query, privateId);
        }
    }
}
