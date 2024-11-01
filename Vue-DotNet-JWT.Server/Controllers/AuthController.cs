using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Vue_DotNet_JWT.Server.DTOs;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace WebApplicationWithOwnJWT.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{

    [HttpPost]
    public IActionResult Register([FromBody] LoginInformation loginInformation)
    {
        return Ok();
    }
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginInformation loginInformation)
    {
        if (loginInformation.UserName == "admin" &&  loginInformation.Password == "admin")
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("WPCBOXRYKSEFUTWL6QT6RXHYD424JFB3"));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, "user@example.com"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

            var token = new JwtSecurityToken(
                issuer: "your-issuer",
                audience: "your-audience",
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: signingCredentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { token = tokenString });
        }
        return Unauthorized("Invalid Credentials");
    }
}