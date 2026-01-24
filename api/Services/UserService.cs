using api.Models.Message;
using api.Models.User;
using api.Repositories;

namespace api.Services
{
    public interface IUserService
    {
        Task<UserResponse> Register(UserRequest request);
        Task<UserResponse> Login(UserRequest request);
        Task<IEnumerable<LoadUser>> LoadUsers();
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

        public async Task<UserResponse> Register(UserRequest request)
        {
            var message = "Failed to create user try again";

            var existingUser = await _userRepository.CheckUsername(request.UserName);

            if (existingUser is not null)
            {
                throw new Exception(message);
            }

            //3 call to httpClient to get avatar from Identicon API

            //passing in a mock url from endpoint to assign Identicon
            //implement httpclient to fetch gravatar below
            //var fetchAvatar = GetAvatar(user.UserName);

            var newUser = new User
            {
                UserName = request.UserName,
                PasswordHash = _passwordHasher.Hash(request.Password),
                IdenticonUrl = request.IdenticonUrl,
                CreatedDate = DateTime.UtcNow
            };

            var query = await _userRepository.CreateUser(newUser);

            if(query)
            {
                var result = await Login(request);
                return result;
            }

            throw new Exception(message);
        }
        public async Task<UserResponse> Login(UserRequest request)
        {
            var message = "Failed to login try again";

            var user = await _userRepository.GetUser(request.UserName);

            if (user is null)
            {
                throw new Exception(message);
            }

            bool verified = _passwordHasher.Verify(request.Password, user.PasswordHash);

            if(!verified)
            {
                throw new Exception(message);
            }

            var token = _tokenService.Create(user.UserName);

            return new UserResponse
            {
                UserId = user.UserId,
                UserName = user.UserName,
                IdenticonUrl = user.IdenticonUrl,
                CreatedDate = user.CreatedDate,
                IsAdmin = user.IsAdmin,
                Token = token
            };
        }
        public async Task<IEnumerable<LoadUser>> LoadUsers()
        {
            var userList = await _userRepository.LoadUsers();

            return userList.Select(u => new LoadUser
            {
                UserId = u.UserId,
                UserName = u.UserName,
                IdenticonUrl = u.IdenticonUrl,
                CreatedDate = u.CreatedDate
            });
        }
    }
}
