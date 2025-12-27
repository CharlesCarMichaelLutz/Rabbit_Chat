using api.Models.User;
using api.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace api.Services
{
    public interface IUserService
    {
        Task<UserResponse> RegisterAsync(string username, string password, string url);
        Task<UserResponse> LoginAsync(string username, string password);
    }
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;
        public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }
        public async Task<UserResponse> RegisterAsync(string username, string password, string url)
        {
            //2 check for existing username
            //3 call to httpClient to get avatar from Identicon API
            //4 hash and salt password 
            //5 add user to DB/Store
            //6 authorize user by generating JWT
            //7 send to Account Controller

            var stored = _userRepository.GetByUsernameAsync(username);

            //returns null if no existing user found
            if (stored is not null)
            {
                var message = "Failed to create username please try again";
                throw new Exception(message);
            }

            //passing in a mock url from endpoint to assign Identicon
                //implement httpclient to fetch gravatar below
                //var fetchAvatar = GetAvatar(user.UserName);

            var newUser = new User
            {
                UserName = username,
                PasswordHash = _passwordHasher.Hash(password),
                IdenticonUrl = url,
                CreatedDate = DateTime.UtcNow
            };

            await _userRepository.CreateUserAsync(newUser);

            var token = _tokenService.Create(username);

            return new UserResponse
            {
               UserName = username,
               Token = token
            };
        }
        public async Task<UserResponse> LoginAsync(string username, string password)
        {
            // 1 check for username in DB
            // 2 verify password hash
            // 3 generate JWT token
            // 4 send to Account Controller

            var user = await _userRepository.GetByUsernameAsync(username);

            if(user is null)
            {
                // Throws an HTTP 500 error
                throw new Exception("The user was not found");
            }

            bool verified = _passwordHasher.Verify(password, user.PasswordHash);

            if(!verified)
            {
                throw new Exception("The password is incorrect");
            }

            var token = _tokenService.Create(username);

            return new UserResponse
            {
                UserName = username,
                Token = token
            };
        }
    }
}
