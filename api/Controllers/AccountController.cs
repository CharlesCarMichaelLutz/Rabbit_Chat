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
            //1 Validate username and pasword with FluentValidation

            //Automatic Validation
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }

            //Manual Validation
            //var validationresult = _validator.Validate(request);

            //if(!validationresult.IsValid)
            //{
            //    return StatusCode(StatusCodes.Status400BadRequest, validationresult.Errors);
            //}

            var registerUser = await _userService.RegisterAsync(request.UserName, request.Password);

            //return Ok(registerUser);
            return Ok(registerUser);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserRequest request)
        {
            var loginUser = await _userService.LoginAsync(request.UserName, request.Password);

            return Ok(loginUser);
        }
    }
}