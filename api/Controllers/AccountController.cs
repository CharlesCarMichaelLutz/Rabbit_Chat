using api.Models;
using api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;


namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(User user )
        {
            var loginUser = await _userService.LoginAsync(user);

            return Ok(loginUser);

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            var validateUser = await _userService.RegisterAsync(user);

            return Ok(validateUser);
        }

    }
}


//    private const string TokenSecret = "secretKeyWillGoHere";
//    private static readonly TimeSpan TokenLifetime = TimeSpan.FromHours(1);

//    [HttpPost("token")]

//    public IActionResult GenerateToken(
//        [FromBody]TokenGenerationRequest request)
//    {
//        var tokenHandler = new JwtSecurityTokenHandler();
//        var key = Encoding.UTF8.GetBytes(TokenSecret);

//    }