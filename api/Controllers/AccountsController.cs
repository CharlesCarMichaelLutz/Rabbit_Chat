using api.Models.User;
using api.Services;
using api.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IValidator<UserRequest> _validator;
        public AccountsController(IUserService userService, IValidator<UserRequest> validator)
        {
            _userService = userService;
            _validator = validator;
        }

        // POST /api/accounts/register
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
            var registerUser = await _userService.Register(request);

            return Ok(registerUser);
        }

        // POST /api/accounts/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserRequest request)
        {
            var loginUser = await _userService.Login(request);

            return Ok(loginUser);
        }

        // GET /api/accounts
        // returns list of users
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.LoadUsers();

            return Ok(users);
        }
    }
}