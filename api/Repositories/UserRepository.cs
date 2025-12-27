using api.Data;
using api.Models.User;
using Dapper;

namespace api.Repositories
{
    public interface IUserRepository
    {
        Task<User> CreateUserAsync(User user);
        Task<User?> GetByUsernameAsync(string username);
    }
    public class UserRepository : IUserRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;
        public UserRepository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<User?> GetByUsernameAsync(string username)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();

            var query = @"SELECT * FROM users WHERE username = @UserName";

            return await connection.QuerySingleOrDefaultAsync<User>(query, username);
        }
        public async Task<User> CreateUserAsync(User user)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();

            var query = @"INSERT INTO users (username, password_hash, identicon_url, created_at) 
                VALUES (@UserName, @PasswordHash, @IdenticonUrl, @CreatedDate)";

            return await connection.QuerySingleAsync<User>(query, user);
        }
    }
}
