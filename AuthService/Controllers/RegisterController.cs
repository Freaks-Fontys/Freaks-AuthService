using AuthService.Database;
using AuthService.Logic;
using AuthService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class RegisterController : ControllerBase
    {
        AuthLogic logic;

        public RegisterController(IConfiguration config, AuthDbContext context)
        {
            logic = new AuthLogic(config, context);
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
