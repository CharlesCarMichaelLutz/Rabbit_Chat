using api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
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
        private readonly IStore _store;
        public UserRepository(IStore store)
        {
            //DI connection to npgsql DB Instance 
            _store = store;
        }
        public async Task<User?> GetByUsernameAsync(string username)
        {
            //check to see if username already exists 
            //return   _store.Users.FirstOrDefault(u => u.UserName == username);
            return await Task.FromResult(_store.Users.FirstOrDefault(u => u.UserName == username));
        }

        public async Task<User> CreateUserAsync(User user)
        {
            _store.Users.Add(user);

            return await Task.FromResult(user);
        }
    }
}
