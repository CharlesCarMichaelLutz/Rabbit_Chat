using api.Models;
using api.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace api.Services
{
    public interface IUserService
    {
        async Task<IActionResult> RegisterAsync(User user);
    }
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IActionResult> RegisterAsync(User user)
        {

            //1 check for length, special characters
                //10 characters in length, 1 capital letter, 1 number, 1 special character 




            //2 check for existing username

            var existingUser = _userRepository.GetByUsernameAsync(user);

            if (existingUser is not null)
            {
                var message = $"A user with username {user.UserName} already exists";
                    //throw new ValidationException(message, new[]
                    //{
                    //    new ValidationFailure(nameof(existingUser), message)
                    //});
                    throw new Exception(message);
            }

            //3 add user to DB/Store

            //4 authorize user by generating JWT

            //5 send user into the application home page
        }

    }
}
