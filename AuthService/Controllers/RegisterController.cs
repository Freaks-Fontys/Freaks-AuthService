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
        ILogger<RegisterController> _logger;
        AuthLogic logic;

        public RegisterController(IConfiguration config, AuthDbContext context, ILogger<RegisterController> logger)
        {
            logic = new AuthLogic(config, context);
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register([FromBody] UserRegister user)
        {
            IActionResult response = BadRequest();
            User newUser = logic.RegisterUser(user);

            if (newUser != null)
            {
                response = Ok(newUser);
            }

            return response;
        }
    }
}
