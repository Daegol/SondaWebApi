using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models.DTOs.Authentication;
using Models.ResponseModels;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        [HttpGet("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<AuthenticationDto>))]
        public IActionResult Authenticate()
        {
            var response = generateJwtToken();
            var responseDto = new AuthenticationDto() { UserId = 1, Token = response };

            return Ok(new BaseResponse<AuthenticationDto>(responseDto, "Pomyślnie zalogowano."));
        }
        private string generateJwtToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("Mega_Super_Secret_Code");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", "1") }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
