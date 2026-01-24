using api.Data;
using api.Models.Message;
using api.Models.User;
using Dapper;

namespace api.Repositories
{
    public interface IUserRepository
    {
        Task<User?> CheckUsername(string username);
        Task<User> GetUser(string username);
        Task<bool> CreateUser(User user);
        Task<IEnumerable<LoadUser>> LoadUsers();
    }
    public class UserRepository : IUserRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;
        public UserRepository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<User?> CheckUsername(string username)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();

            const string sql =
                """
                SELECT
                    user_id       AS UserId,
                    username      AS UserName,
                    password_hash AS PasswordHash,
                    identicon_url AS IdenticonUrl,
                    created_at    AS CreatedDate,
                    is_admin      AS IsAdmin
                FROM users WHERE username = @UserName LIMIT 1
                """;

            var user = await connection.QuerySingleOrDefaultAsync<User>(sql, new { username });

            return user;
        }
        public async Task<bool> CreateUser(User user)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();

            const string sql = 
                """
                INSERT INTO users (username, password_hash, identicon_url, created_at) 
                VALUES (@UserName, @PasswordHash, @IdenticonUrl, @CreatedDate)
                """;

            var result = await connection.ExecuteAsync(sql, user);

            return result > 0;
        }
        public async Task<User> GetUser(string username)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();

            const string sql =
                """
                SELECT
                    user_id       AS UserId,
                    username      AS UserName,
                    password_hash AS PasswordHash,
                    identicon_url AS IdenticonUrl,
                    created_at    AS CreatedDate,
                    is_admin      AS IsAdmin
                FROM users WHERE username = @UserName LIMIT 1
                """;

            var user = await connection.QuerySingleAsync<User>(sql , new { username });

            return user;
        }
        public async Task<IEnumerable<LoadUser>> LoadUsers()
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();

            const string sql =
                """
                SELECT 
                    user_id AS UserId, 
                    username AS UserName, 
                    identicon_url AS IdenticonUrl,
                    created_at AS CreatedDate
                FROM users 
                ORDER BY created_at
                """;

            return await connection.QueryAsync<LoadUser>(sql);
        }
    }
}
