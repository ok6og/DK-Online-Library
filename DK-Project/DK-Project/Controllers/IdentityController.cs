using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookStore.BL.Interfaces;
using DK_Project.Models.Configurations;
using DK_Project.Models.Models.Users;
using DK_Project.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DK_Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IIdentityService _identityService;
        private readonly RoleManager<UserRole> _roleManager;
        private readonly IOptions<MyJsonSettings> _jsonSettings;
        public IdentityController(IConfiguration configuration, IIdentityService identityService, RoleManager<UserRole> roleManager, IOptions<MyJsonSettings> jsonSettings)
        {
            _configuration = configuration;
            _identityService = identityService;
            _roleManager = roleManager;
            _jsonSettings = jsonSettings;
        }
        [AllowAnonymous]
        [HttpPost(nameof(CreateUser))]
        public async Task<IActionResult> CreateUser([FromBody] UserInfo user)
        {
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest($"Username or password is missing");
            }
            var result = await _identityService.CreateAsync(user);
            return result.Succeeded ? Ok(result) : BadRequest(result);

        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            if (loginRequest != null && !string.IsNullOrEmpty(loginRequest.UserName)&&!string.IsNullOrEmpty(loginRequest.Password))
            {
                var user = await _identityService.CheckUserAndPassword(loginRequest.UserName, loginRequest.Password);
                if (user != null)
                {
                    var userRoles = await _identityService.GetUserRoles(user);
                    var claims = new List<Claim>
                    {
                        new Claim(JwtRegisteredClaimNames.Sub,_configuration.GetSection("Jwt:Subject").Value),
                        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat,DateTime.Now.ToString()),
                        new Claim("UserId",user.UserId.ToString()),
                        new Claim("DisplayName",user.DisplayName??string.Empty),
                        new Claim("UserName",user.UserName??string.Empty),
                        new Claim("Email",user.Email??string.Empty),
                        new Claim("Admin", "Admin")
                    };

                    foreach (var role in userRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }



                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.Now.AddMinutes(10), signingCredentials: signIn);
                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest("Username or password is missing");
            }
        } 

    }
}
