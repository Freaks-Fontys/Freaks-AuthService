using AuthService.Database;
using AuthService.Logic;
using AuthService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly AuthLogic logic;
        private readonly UserServiceClient _client;
        private readonly ILogger _logger;
        

        public RegisterController(IConfiguration config, ILogger<RegisterController> logger, AuthDbContext context, UserServiceClient client)
        {
            logic = new AuthLogic(config, context);
            _client = client;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserRegister user)
        {
            _logger.LogDebug("User will be created");
            IActionResult response = BadRequest();
            User dbUser = logic.RegisterUser(user);
            if (dbUser != null)
            {
                 await _client.SendUserCreated(dbUser);
                response = Ok(dbUser);
            }

            return response;
        }
    }
}
