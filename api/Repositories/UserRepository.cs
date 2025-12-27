using api.Data;
using api.Models.User;

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
            //DI connection to npgsql DB Instance 
            _connectionFactory = connectionFactory;
        }
        public async Task<User?> GetByUsernameAsync(string username)
        {
            //check to see if username already exists 

            using var connection = await _connectionFactory.CreateConnectionAsync();
            
            return null;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            //return await Task.FromResult(user);

            using var connection = await _connectionFactory.CreateConnectionAsync();

            return null;
        }
    }
}
