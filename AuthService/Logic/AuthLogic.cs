using AuthService.Database;
using AuthService.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Logic
{
    public class AuthLogic
    {
        IConfiguration _config;
        private AuthDbContext _context;

        public AuthLogic(IConfiguration config, AuthDbContext context)
        {
            _config = config;
            _context = context;
        }

        public  string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        

        public User AuthenticateUser(UserLogin login)    
        {    
            User user = null;

            try
            {
                var efResult = _context.Users.Where(x => x.Email == login.Email).ToArray();
                user = efResult[0];
                if(user.Email == login.Email && user.Password == login.Password)
                {
                    user.LatestLoginAt = DateTime.Now;
                    _context.SaveChanges();
                    return user;
                }
                else
                {
                    throw new UnauthorizedAccessException();
                }
                
            }
            catch
            {
                return user = null;
            } 
        }

        public User RegisterUser(UserRegister user)
        {
            User newUser = null;
            var dbCheck = _context.Users.Where(x => x.Email == user.Email).DefaultIfEmpty().ToArray();

            if (dbCheck[0] == null && user.Password.Length > 5)
            {
                newUser = new User {
                    Id = Guid.NewGuid(),
                    UserName = user.UserName,
                    Email = user.Email,
                    Password = user.Password,
                    AvatarURL = user.AvatarURL,
                    RegisteredAt = DateTime.Now
                };
                _context.Users.Add(newUser);
                _context.SaveChanges();
                return newUser;
            }

            return newUser;
        }

    }
}
