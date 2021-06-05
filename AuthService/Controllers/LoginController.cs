using AuthService.Database;
using AuthService.Logic;
using AuthService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        AuthLogic logic;

        public LoginController(IConfiguration config,  AuthDbContext context)
        {
            logic = new AuthLogic(config, context);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserLogin login)
        {
            IActionResult response = Unauthorized();
            var user = logic.AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = logic.GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }
    }
}
