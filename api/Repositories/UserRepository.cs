using api.Models;
using api.Data ;
using Microsoft.AspNetCore.Mvc;

namespace api.Repositories
{
    public interface IUserRepository
    {
        public User CreateAsync(User user);

        public User GetByUsernameAsync(string username);
    }
    public class UserRepository : IUserRepository
    {
        private readonly IStore _store;
        public UserRepository(IStore store)
        {
            //DI connection to npgsql DB Instance 
            _store = store;
        }
        public User CreateAsync(User user)
        {
            _store.Users.Add(user);

            return user;
        }

        public User GetByUsernameAsync(string username)
        {
            //check to see if username already exists 
           var user = _store.Users.FirstOrDefault(u => u.UserName == username);

            return user;
        }
    }
}
