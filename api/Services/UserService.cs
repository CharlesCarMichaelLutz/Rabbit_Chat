using api.Models;
using api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace api.Services
{
    public interface IUserService
    {
        Task<UserDto> RegisterAsync(User user);
        Task<UserDto> LoginAsync(User user);
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
        public async Task<UserDto> RegisterAsync(User user)
        {

            //1 Validate username and pasword with FluentValidation

            //2 check for existing username
            var existingUser = _userRepository.GetByUsernameAsync(user.UserName);

            //returns null if no existing user found

            if (existingUser is not null)
            {
                var message = $"An account with username {user.UserName} already exists";
                    throw new Exception(message);
            }

            //call to httpClient to get avatar from Identicon API
            //var fetchAvatar = GetAvatar(user.UserName);

            var addUser = new User
            {
                UserName = user.UserName,
                //3 hash and salt password 
                PasswordHash = _passwordHasher.Hash(user.PasswordHash),
                //4 query Identicon Api endpoint to generate avatar 
                CreatedDate = DateTime.UtcNow
            };

            //5 add user to DB/Store

            var result = _userRepository.CreateAsync(addUser);


            //4 authorize user by generating JWT

            //5 send user into the application home page
            return new UserDto
            {
                UserName = result.UserName,
                Token = _tokenService.Create(result)
            };
        }

        public async Task<UserDto> LoginAsync(User user)
        {
            // 1 check for the username in the DB
            var loginRequest = _userRepository.GetByUsernameAsync(user.UserName);
                
            if(loginRequest is null)
            {
                throw new Exception("The user was not found");
            }

            // 2 verify the password hash
            bool verified = _passwordHasher.Verify(user.PasswordHash, loginRequest.PasswordHash);

            if(!verified)
            {
                throw new Exception("The password is incorrect");
            }

            //3 generate JWT token

            return new UserDto
            {
                UserName = loginRequest.UserName,
                Token = _tokenService.Create(loginRequest)
            };
        }

    }
}
