using api.Models.User;
using api.Services;
using api.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IValidator<UserRequest> _validator;
        public AccountController(IUserService userService, IValidator<UserRequest> validator)
        {
            _userService = userService;
            _validator = validator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
            var registerUser = await _userService.RegisterAsync(request);

            return Ok(registerUser);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserRequest request)
        {
            var loginUser = await _userService.LoginAsync(request);

            return Ok(loginUser);
        }
    }
}