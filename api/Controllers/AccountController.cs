using api.Models;
using api.Services;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;


namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IValidator<UserRequestDto> _validator;

        public AccountController(IUserService userService, IValidator<UserRequestDto> validator)
        {
            _userService = userService;
            _validator = validator;
        }

        [HttpPost("login")]
        public async Task<IResult> Login(UserRequestDto request)
        {
            var loginUser = await _userService.LoginAsync(request);

            return Ok(loginUser);
        }

        [HttpPost("register")]
        public async Task<IResult> Register(UserRequestDto request)
        {
            //1 Validate username and pasword with FluentValidation

            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var registerUser = await _userService.RegisterAsync(request);

            return Ok(registerUser);
        }
    }
}