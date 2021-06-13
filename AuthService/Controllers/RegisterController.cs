using AuthService.Database;
using AuthService.Logic;
using AuthService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly ILogger<RegisterController> _logger;
        private readonly AuthLogic logic;

        public RegisterController(IConfiguration config, AuthDbContext context, ILogger<RegisterController> logger)
        {
            logic = new AuthLogic(config, context);
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register([FromBody] UserRegister user)
        {
            _logger.LogDebug($"/user/register endpoint called with user {user?.Email}", user);
            IActionResult response = BadRequest();
            User dbUser = logic.RegisterUser(user);

            if (dbUser != null)
            {
                _logger.LogTrace($"user with email {dbUser.Email} registered with id {dbUser.Id}");
                response = Ok(dbUser);
            }
            else
            {
                _logger.LogDebug($"no user created with email {dbUser.Email}");
            }

            return response;
        }
    }
}
