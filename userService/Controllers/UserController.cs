using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using userService.Client;
using userService.Data;
using userService.Models;
using userService.Repositories;

namespace userService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly UserContext _context;
        private readonly ICartClient _cartCliet;
        private readonly ILogger<UserController> _logger;
        public UserController(IUserRepository repository, IConfiguration configuration,UserContext context,ICartClient cartClient,ILogger<UserController> logger)
        {
            _repository = repository;
            _configuration = configuration;
            _context = context;
            _cartCliet = cartClient;
            _logger =logger;


        }
        
        [HttpGet("getuser")]
        public async Task<IActionResult> GetUsers()
        {
            var res = await _repository.GetAllUsers();
            return Ok(res);
        }

        [HttpPost("login")]

        public async Task<dynamic> Login([FromForm]User request)
        {
            var user = _context.Users.Where(x=>x.Email==request.Email).FirstOrDefault();

            if (user != null)
            {
                try
                {
                    var response = await _cartCliet.SendUSerDetails(user);
                    if (response)
                    {
                        _logger.LogInformation("connection to inventory service established");
                    }
                    else
                    {
                        return BadRequest("unable to connect to invetory service");
                    }

                }
                catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else {
                return Ok("UserName or password did not Match");
            }
            

            if (user.UserType == "Admin")
            {
                var authClaims = new List<Claim>
                {
                    new Claim("UserId", user.Id.ToString()),
                    new Claim("Email", user.Email),
                    new Claim("Role", "Admin")
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

                return Ok(new {token=Jwt });
            }
            else
            {
                var result = _repository.Login(user);
                if (result == null)
                {
                    var response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                    response.Content = new StringContent("user not Found");
                    return response;
                }
                return result;
            }

            
        }
    
    }

}
