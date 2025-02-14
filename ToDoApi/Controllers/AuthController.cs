using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoApi.Shared.Interfaces;
using ToDoApi.Shared.Models;
using ToDoApi.Shared.Models.ResponseModels;

namespace ToDoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthService _authService;

        public AuthController(
            ILogger<AuthController> logger,
            IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserModel user)
        {
            if (user is null)
            {
                return new UnauthorizedResult();
            }

            var token = _authService.Authenticate(user.UserName, user.Password);
            if (token == null)
                return Unauthorized();

            return Ok(new LoginResponse { Token = token });
        }

    }
}