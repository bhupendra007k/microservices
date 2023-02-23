using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using userService.Data;
using userService.Models;

namespace userService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context;
        private readonly IConfiguration _configuration;
        public UserRepository(UserContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

        }
        public async Task<IList<User>> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public async Task<string> Login(User request)
        {
            var user =_context.Users.FirstOrDefault(u => u.Email == request.Email);
            if (user == null)
            {
                return null;
            }

            var authClaims = new List<Claim>
            {
                new Claim("Email",request.Email),

                new Claim("UserId",request.Id.ToString()),

                new Claim("Role", "Customer"),

            };



            var authSigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256Signature)


                );
            var Jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return Jwt;

        }
    }
}
