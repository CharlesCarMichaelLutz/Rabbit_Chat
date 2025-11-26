using api.Models;
using api.Repositories;

using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace api.Services
{
    public interface IUserService
    {
        Task<UserResponseDto> RegisterAsync(UserRequestDto userDto);
        Task<UserResponseDto> LoginAsync(UserRequestDto user);
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
        public async Task<UserResponseDto> RegisterAsync(UserRequestDto request)
        {
            //2 check for existing username
            //3 call to httpClient to get avatar from Identicon API
            //4 hash and salt password 
            //5 query Identicon Api endpoint to generate avatar 
            //6 add user to DB/Store
            //7 authorize user by generating JWT
            //8 send to Account Controller

            var stored = _userRepository.GetByUsernameAsync(request.UserName);

            //returns null if no existing user found
            if (stored is not null)
            {
                var message = $"An account with username {request.UserName} already exists";
                    throw new Exception(message);
            }

            //var fetchAvatar = GetAvatar(user.UserName);

            var user = new User
            {
                UserName = request.UserName,
                PasswordHash = _passwordHasher.Hash(request.Password),
                CreatedDate = DateTime.UtcNow
            };

            var result = _userRepository.CreateAsync(user);

            return new UserResponseDto
            {
                UserName = result.UserName,
                Token = _tokenService.Create(result)
            };
        }
        public async Task<UserResponseDto> LoginAsync(UserRequestDto request)
        {
            // 1 check for username in DB
            // 2 verify password hash
            // 3 generate JWT token
            // 4 send to Account Controller

            var loginRequest = _userRepository.GetByUsernameAsync(request.UserName);
                
            if(loginRequest is null)
            {
                // Throws an HTTP 500 error
                throw new Exception("The user was not found");
            }

            bool verified = _passwordHasher.Verify(request.Password, loginRequest.PasswordHash);

            if(!verified)
            {
                throw new Exception("The password is incorrect");
            }

            return new UserResponseDto
            {
                UserName = loginRequest.UserName,
                Token = _tokenService.Create(loginRequest)
            };
        }
    }
}
