using api.Data;
using api.Models.User;
using Dapper;

namespace api.Repositories
{
    public interface IUserRepository
    {
        Task<User?> CheckUsernameAsync(string username);
        Task<User> GetUser(string username);
        Task<bool> CreateUserAsync(User user);
    }
    public class UserRepository : IUserRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;
        public UserRepository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<User?> CheckUsernameAsync(string username)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();

            var user =await connection.QuerySingleOrDefaultAsync<User>(
                """
                SELECT
                    user_id       AS UserId,
                    username      AS UserName,
                    password_hash AS PasswordHash,
                    identicon_url AS IdenticonUrl,
                    created_at    AS CreatedDate,
                    is_admin      AS IsAdmin
                FROM users WHERE username = @UserName LIMIT 1
                """, new { username });

            return user;
        }
        public async Task<bool> CreateUserAsync(User user)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();

            var query = @"INSERT INTO users (username, password_hash, identicon_url, created_at) 
                VALUES (@UserName, @PasswordHash, @IdenticonUrl, @CreatedDate)";

            var result = await connection.ExecuteAsync(query, user);

            return result > 0;
        }
        public async Task<User> GetUser(string username)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();

            var user = await connection.QuerySingleAsync<User>(
                """
                SELECT
                    user_id       AS UserId,
                    username      AS UserName,
                    password_hash AS PasswordHash,
                    identicon_url AS IdenticonUrl,
                    created_at    AS CreatedDate,
                    is_admin      AS IsAdmin
                FROM users WHERE username = @UserName LIMIT 1
                """, new { username });

            return user;
        }
    }
}
