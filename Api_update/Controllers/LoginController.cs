using CashbackApi.Models;
using CashbackApi.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.CodeDom.Compiler;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CashbackApi.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase {
        private IConfiguration _config;
        public LoginController(IConfiguration config) {
            _config = config;
        
        }
        [AllowAnonymous]
        [HttpPost("token")]
        public IActionResult Login([FromBody] UserLogin userlogin) {  
            var user = Authenticate(userlogin);
            if (user != null) {
                var token = Generate(user);
                return Ok(token);
            }
            return NotFound("User not found");
        
        }
        private object Generate(UserModel user) {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
             _config["Jwt:Audience"],
             claims,
             expires: DateTime.Now.AddMinutes(15),
             signingCredentials: credentials);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(token);
            Response.Cookies.Append("token", encodedJwt);
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
        private UserModel Authenticate(UserLogin userLogin) {
            var currentUser = UserConst.User.FirstOrDefault(o => o.Username.ToLower()==
            userLogin.Login.ToLower() && o.Password==userLogin.Password);
            if (currentUser != null) {
                return currentUser;
            }
            return null;
        }
    }
}
