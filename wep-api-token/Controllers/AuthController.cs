using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using wep_api_token.models;
using wep_api_token.models.DTOs;
using wep_api_token.services;

namespace wep_api_token.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserServices userServices;

        public AuthController(IConfiguration configuration , UserServices userServices)
        {
            _configuration = configuration;
            this.userServices = userServices;
        }    
                

        [HttpPost("Login")]
        public async Task<ActionResult<Response>> Login(UserDto user)
        {
             if(user.UserName == "" && user.Password == "")
            {
                return BadRequest("user not admite campos vacios");  
            }

            
            var result = await userServices.Login(user);
            if (result.Success)
            {
              result.data = new{
                    token = generateTokenTwo(user.UserName)
              };
                return Ok(result);
            }

            return Unauthorized(result);
        }

        [Authorize]
        [HttpGet("refreshToken")]
        public ActionResult<Response> RefreshToken()
        {
            //var userNameClaim = HttpContext.User?.Claims.Where(claim => claim.Type == ClaimTypes.Name).Select(claim => claim.Value).ToList();
            var userName = User.FindAll(ClaimTypes.Email);
            var userNameFinal = userName.Select(c => c.Value).ToList();
            // de esta manera puedo seleccionar cualquier claim y usarla  
            var userId = User.FindAll("sub");
            var userNameId= userName.Select(c => c.Value).ToList();

            var newToken = generateTokenTwo(userNameFinal[0].ToString());

            return new Response
            {
                Success = true,
                data = newToken,
                Message  = "agui bb"
            };
        }
        
        private string generateToken(UserDto user)
        {


            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role,"admin")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("Jwt:Key").Value!));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(20),
                    signingCredentials: cred

                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;

        }

  
        private string generateTokenTwo(string userName)
        {
            var issuer = _configuration.GetSection("Jwt:Issuer").Value;
            var audience = _configuration.GetSection("Jwt:Audience").Value;
            var key = Encoding.ASCII.GetBytes
            (_configuration.GetSection("Jwt:Key").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim("sub", userName),
                new Claim(JwtRegisteredClaimNames.Name, userName),
                new Claim(JwtRegisteredClaimNames.Email, userName),
                new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString())
             }),
                Expires = DateTime.UtcNow.AddDays(1),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }    


    }
}
