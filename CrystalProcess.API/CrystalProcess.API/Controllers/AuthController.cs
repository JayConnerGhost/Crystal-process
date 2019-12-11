using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CrystalProcess.API.Requests;
using CrystalProcess.Models;
using CrystalProcess.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace CrystalProcess.API.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthRepository repo, IConfiguration config, ILogger<AuthController> logger)
        {
            _repo = repo;
            _config = config;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]  UserForRegisterRequest userForRegister)
        {
            userForRegister.Username = userForRegister.Username.ToLower();
            if (await _repo.UserExists(userForRegister.Username))
            {
                return BadRequest("Username already exists");
            }

            var userToCreate = new User
            {
                Username = userForRegister.Username
            };
            var createdUser = await _repo.Register(userToCreate, userForRegister.Password);

            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]UserForLoginRequest userForLogin)
        {
            User userFromRepo=null;
            SigningCredentials creds = null;
            
            try
            {
                userFromRepo = await _repo.Login(userForLogin.Username.ToLower(), userForLogin.Password);

                if (userFromRepo == null)
                {
                    return Unauthorized();
                }

               
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Secret").Value));

                creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            }
            catch (Exception e)
            {
                _logger.LogError(Guid.NewGuid().ToString(),e);
            }
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),

                new Claim(ClaimTypes.Name, userFromRepo.Username)
            };
            claims.AddRange(userFromRepo.Roles.Select(role => new Claim(ClaimTypes.Role, role.Name.ToString())));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken((tokenDescriptor));

            return Ok(new
            {
                token = tokenHandler.WriteToken(token),
                userFromRepo.Username
            });


        }
    }
}